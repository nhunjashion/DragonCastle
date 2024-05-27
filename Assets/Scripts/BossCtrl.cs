using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : MonoBehaviour
{
    
    
    [SerializeField] private float _speed = 10;  
    [SerializeField] private float _baseAttackDmg;
    [SerializeField] private float _attackDmg;
    [SerializeField] protected float _attackRange;
    public Transform attackPoint;

    public Transform pivot;
    public Vector2 pivotDir;

    //attack Player 
    [SerializeField] private Vector2 _playerTarget;
    [SerializeField] private LayerMask _whatIsPlayer;
    bool isDetectPlayer;
    bool canLeap;
    [SerializeField] protected float coolDown = 4f;

    //Boss jump
    [SerializeField] private Transform _groundCheck;
    public float groundCheckRadius;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private bool _canJump = false;
    bool isTouchingGround;

    //boss Fly
    private bool _canFly=false;
    [SerializeField] private float _flyDistance = 50f;

    bool isFacingLeft = true;

    Rigidbody2D rb;


    public float timeReset = 50;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(10);

            }
        } 
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        CheckOnGround();

        FlipTowardPlayer();
       // FlyUp();
        Leaping();
        ResetEnemy();
    }

    void CheckOnGround()
    {
        isTouchingGround = Physics2D.OverlapCircle(_groundCheck.position, groundCheckRadius, _whatIsGround);
        isDetectPlayer = Physics2D.OverlapCircle(attackPoint.position, _attackRange, _whatIsPlayer);
    }

   

    protected virtual void FlyUp()
    {
        Vector2 pos = new Vector2(transform.position.x, _flyDistance);
        if(_canFly)
        {
            _canFly = false;
            rb.MovePosition(pos);
        }
    }

    protected virtual void Leaping()
    {
        coolDown -= Time.deltaTime;
        if(coolDown <= 0)
        {
            canLeap = true;
        }

        if(canLeap && isDetectPlayer)
        {       
            canLeap = false;
            _playerTarget = InputForEnemy.Instance.PlayerPos - transform.position;

            _playerTarget.Normalize();

            rb.velocity = _playerTarget * _speed;

            coolDown = 4;
        }
    }

    void ResetEnemy()
    {
        timeReset -= Time.deltaTime;

        if(timeReset <= 0)
        {
            transform.position = pivot.transform.position;
            timeReset = 50;
        }
    }

    void FlipTowardPlayer()
    {
        float playerDir = InputForEnemy.Instance.PlayerPos.x - transform.position.x;

        if(playerDir > 0 && isFacingLeft)
        {
            Flip();
        }
        else if(playerDir <0 && !isFacingLeft)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingLeft = !isFacingLeft;
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }
    

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_groundCheck.position,groundCheckRadius); 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position,_attackRange);
    }

}
