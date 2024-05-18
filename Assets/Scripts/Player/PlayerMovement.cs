using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    protected static PlayerMovement instance;
    public static PlayerMovement Instance {get => instance;}


    [SerializeField] private float gravityScale = 3f;
    [SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
    [SerializeField] protected float speed = 50f;
    [SerializeField] protected Vector2 targetPos;
    [SerializeField] protected float jumpForce;
    [SerializeField] protected float _baseJumpForce = 500f;
    private bool _isJumpOnTramp = false;


    [SerializeField] private Transform groundCheck;
    [Range(0f, .3f)][SerializeField] private float movementSmoothing = .05f;


    const float k_GroundedRadius = .6f; // Radius of the overlap circle to determine if grounded

    protected bool isFacingRight = true;
    
  //  private bool isJump = false;
    private bool canJump = true;
    private bool canAutoJump = false;
    private float horizontalMove = 0f;
    public bool Grounded;
    private Vector3 Velocity = Vector3.zero;


    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sr;
    CapsuleCollider2D coll;

    [Space]
    public UnityEvent OnLandEvent;

    private bool disableMovement = false;

    //Wall jump + wall sliding
    public Transform wallCheck;
    bool isWallTouch;
    bool isSliding;
    public float wallSlidingSpeed;
    public float wallJumpDuration;
    public float wallJumpForce;
    public bool wallJumping;
    float input;
    [SerializeField] protected float wallCheckDistance = 0.5f;
    [SerializeField] protected Vector2 wallJumpDirection;

    private KnockBack kb;

    private void Awake()
    {
        Debug.Log("aWAKE");
        sr = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        if(OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            jumpForce = _baseJumpForce;
            Grounded = true;
            canAutoJump = false;
            OnLandEvent.Invoke();
        }

        if(collision.gameObject.CompareTag("Tramp"))
        {
            canAutoJump = true;
            _isJumpOnTramp = true;
            OnLandEvent.Invoke();

        }

    }

    private void Start()
    {
        Debug.Log("Start");
        PlayerMovement.instance = this;
        anim = GetComponent<Animator>();
        kb = GetComponent<KnockBack>();
    }


    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;
    }


    private void FixedUpdate()
    {
    //    this.CheckOnGround();
        FlipAnim();
        this.GetTargetPos();
        if(!kb.isBeingKnockBack)
        {
            this.Moving();
            this.Jumping();
        }

        this.AutoJump();
    }


    protected virtual void GetTargetPos()
    {
        this.targetPos.x = InputManager.Instance.Horizontal;
        this.targetPos.y = 0;
    }
      

    protected virtual void Moving()
    {
        FlipAnim();
        anim.SetFloat("Speed", Mathf.Abs(InputManager.Instance.Horizontal * speed));

        horizontalMove = InputManager.Instance.Horizontal * speed;
        Vector3 targetVelocity = new Vector2(horizontalMove * Time.fixedDeltaTime * 10f, rb.velocity.y);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref Velocity, movementSmoothing);
    }

    public void CheckOnGround()
    {
       bool wasGrounded = Grounded;
        Grounded = false;

        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(groundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i=0; i< collider2Ds.Length; i++)
        {
            if(collider2Ds[i].gameObject != gameObject)
            {
                Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        } 


        if(canJump && _isJumpOnTramp)
        {
            OnLandEvent.Invoke();
        }
    }

    float CurrentJumpForce()
    {
        if( _isJumpOnTramp)
        {
            jumpForce += 200;
        }
      //  else { return _baseJumpForce; }
        if(jumpForce >= 1800)
        {
            jumpForce = 1800;
        }
        return jumpForce;
    }

    protected virtual void AutoJump()
    {
        if (canAutoJump && _isJumpOnTramp)
        {
            anim.SetBool("IsJumping", true);
            canAutoJump = false;
            rb.AddForce(new Vector2(0f, CurrentJumpForce()));
        }
    }

    protected virtual void Jumping()
    {
        if(!canAutoJump && canJump && InputManager.Instance.Jump   )
        {        
            
            rb.AddForce(new Vector2(rb.velocity.x, _baseJumpForce));

            anim.SetBool("IsJumping", true);

            canJump = false;


           // rb.velocity=(new Vector2(0f, _baseJumpForce));
        }      
    }


    public void OnLanding()
    {
        anim.SetBool("IsJumping", false);
    }

    public void FlipAnim()
    {
        if(InputManager.Instance.Horizontal >0  && !isFacingRight)
        {
            Flip();
        }  
        if(InputManager.Instance.Horizontal <0 && isFacingRight)
        {
            Flip();
        }

    }    
    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }
 
}
