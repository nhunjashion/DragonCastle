using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmfulBulletCtrl : MonoBehaviour
{
    private float Timer = 3;

    private void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if(collision.gameObject.CompareTag("Ground"))
            {
                    Destroy(gameObject);   
            }
        }

        IDamageable iDamageable = collision.gameObject.GetComponent<IDamageable>();
        if(iDamageable != null)
        {
            iDamageable.Damage(0, (InputForEnemy.Instance.PlayerPos - transform.localPosition).normalized);
        }
    }
}
