using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashPower = 100f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    [SerializeField] private TrailRenderer tr;

    private Rigidbody2D rb;
    Collider2D col;
    private void Start()
    {
        rb= GetComponent<Rigidbody2D>(); 
        col= GetComponent<CapsuleCollider2D>();
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


    private void Update()
    {

            if (isDashing)
            {
                
                return;
            }
            if(InputManager.Instance.Dashing && canDash)
            {
                StartCoroutine(Dashing());    
                if(col.gameObject.CompareTag("Ground") && isDashing)
                {
                    rb.velocity=Vector2.zero;
                }
            }
        
        
    }

    private IEnumerator Dashing()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        tr.emitting=false;
        rb.gravityScale = originalGravity;
        isDashing=false;
        yield return new WaitForSeconds(dashCooldown);
        canDash=true;
    }
}
