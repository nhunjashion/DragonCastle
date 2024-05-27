using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SwordSlash : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firePoint;

    public TMP_Text ultiCD;
    public GameObject bgCD;



    Vector3 bulletDir;
    public float bulletSpd = 50f;
    float delaytime = 0.1f;
    public float countDown = 15f;

    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        countDown = 0f;
    }
    private void Update()
    {
        Shooting();
        countDown -= Time.deltaTime;
        ultiCD.text = ((int)countDown).ToString();

        if(countDown < 0)
        {
            countDown = 0;
            bgCD.SetActive(false);
        }
    }

    void Shooting()
    {
        if (InputManager.Instance.ShootBeam && countDown <=0)
        {
            bgCD.SetActive(true);
            countDown = 15f;
            anim.SetTrigger("Ulti");
            AudioManager.instance.PlaySFX(AudioManager.instance.ultimate);
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
