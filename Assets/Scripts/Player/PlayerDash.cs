using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private bool canDash = true;
    private bool isDashing;
    private float dashPower = 100f;
    private float dashTime = 0.2f;
    private float dashCooldown = 1f;

    [SerializeField] private TrailRenderer tr;
    private Rigidbody2D rb;
    private void Start()
    {
        rb= GetComponent<Rigidbody2D>();    
    }

    private void Update()
    {

            if (isDashing)
            {
                return;
            }
            if(InputManager.Instance.Dashing)
            {
                StartCoroutine(Dashing());
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
