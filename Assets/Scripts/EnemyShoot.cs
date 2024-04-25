using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firePoint;
    private float Timer = 1f;

    private void Update()
    {

        Shoot();
    }

    private void Shoot()
    {

        Timer -= Time.deltaTime;
        if (Timer <=0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.left * 20;
            Timer = 1f;
        }
        
    }
}
