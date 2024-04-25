using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firePoint;
    Vector3 bulletDir;
    public float bulletSpd = 50f;
    private void Update()
    {
        Shooting();
    }

    void Shooting()
    {
        if (InputManager.Instance.RangedAttack)
        {        
            GameObject bulletSlash = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            Rigidbody2D rb = bulletSlash.GetComponent<Rigidbody2D>();
            bulletDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            bulletDir.z = 0;
            bulletDir = bulletDir.normalized;

            rb.velocity = bulletDir * bulletSpd * Time.deltaTime;
            Debug.Log("FiREEEEEEEEEE");
        }
    }
}
