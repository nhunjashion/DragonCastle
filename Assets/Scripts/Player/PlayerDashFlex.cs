using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashFlex : MonoBehaviour
{
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashPower = 50f;
    private float dashTime = 0.2f;
    private float dashCooldown = 2f;
    private Vector3 target;

    [SerializeField] private TrailRenderer tr;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = transform.position;
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }
        if (InputManager.Instance.Dashing)
        {
            StartCoroutine(FlexDashing());
        }
    }

    
    private IEnumerator FlexDashing()
    {
        target = InputManager.Instance.MousePos;
        target.z = transform.position.z;

        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        transform.position = Vector3.MoveTowards(transform.position, target, dashPower);
       // rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
