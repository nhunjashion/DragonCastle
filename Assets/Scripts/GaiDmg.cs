using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaiDmg : MonoBehaviour
{
    int dmg = 10;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                 IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
                if(damageable != null)
                {
                    damageable.Damage(dmg);
                    Debug.Log("player hurt!");
                }
            }
        }

    }
}
