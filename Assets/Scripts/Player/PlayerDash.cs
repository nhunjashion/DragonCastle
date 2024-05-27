using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public static PlayerDash instance;

    private bool canDash = true;
    public bool isDashing;
    [SerializeField] private float dashPower = 100f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    [SerializeField] private TrailRenderer tr;

    private Rigidbody2D rb;
    Collider2D col;
    Animator anim;
    private void Start()
    {
        PlayerDash.instance = this;
        rb= GetComponent<Rigidbody2D>(); 
        col= GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision!=null)
        {
            if(collision.gameObject.CompareTag("Ground"))
            {
                canDash = false;
            }

        }
    }


    public void Update()
    {

            if (isDashing)
            {
                
                return;
            }
            if(InputManager.Instance.Dashing && canDash)
            {

                anim.SetBool("isDashing", true);
                StartCoroutine(Dashing());  
                
                if(col.gameObject.CompareTag("Ground") && isDashing)
                {
                    anim.SetBool("isDasing", false);
                    rb.velocity=Vector2.zero;
                }
            }

        
    }

    public IEnumerator Dashing()
    {
        PlayerHealth.Instance.canGetDmg = false;
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);

        AudioManager.instance.PlaySFX(AudioManager.instance.dash);
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        tr.emitting=false;
        rb.gravityScale = originalGravity;
        isDashing=false;
        yield return new WaitForSeconds(dashCooldown);
        anim.SetBool("isDashing", false);
        canDash=true;
        PlayerHealth.Instance.canGetDmg = true;
    }
}
