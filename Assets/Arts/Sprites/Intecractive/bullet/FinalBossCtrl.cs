using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalBossCtrl : MonoBehaviour, IDamageable
{

    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected float _attackRange;
    [SerializeField] private LayerMask _whatIsPlayer;
    public Transform attackPoint;
    public bool isDetectPlayer;

    Vector3 bulletDir;

    public float bulletSpd = 200f;
    float delaytime = 0.1f;
    public float countDown = 1.5f;

    public GameObject uiHP;

    public float timeReset=30f;

    [SerializeField] private Slider hpBar;

    [SerializeField] private int _maxHP = 1000;
    [SerializeField] public int currentHP;
    private void Start()
    {
        currentHP = _maxHP;
        countDown = 0f;
    }

    private void Update()
    {
        CheckPlayer();
        Shooting();
        countDown -= Time.deltaTime;
        SetHPValue();

    }



    void CheckPlayer()
    {
        isDetectPlayer = Physics2D.OverlapCircle(attackPoint.position, _attackRange, _whatIsPlayer);
    } 


    void Shooting()
    {
        if (isDetectPlayer && countDown <= 0)
        {
            uiHP.SetActive(true);
            countDown = 1.5f;
            GameObject bulletSlash = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            Rigidbody2D rb = bulletSlash.GetComponent<Rigidbody2D>();
            bulletDir = InputForEnemy.Instance.PlayerPos - transform.position;
            bulletDir.z = 0;
            bulletDir = bulletDir.normalized;

            rb.velocity = bulletDir * bulletSpd * Time.deltaTime;


        }
        if(!isDetectPlayer)
        {
            uiHP.SetActive(false);
            timeReset -= Time.deltaTime;
        }

        if(timeReset <= 0)
        {
            currentHP = _maxHP;
            timeReset = 30f;
        }

    }


    public void SetHPValue()
    {
        if (hpBar.value != currentHP)
        {
            hpBar.value = (float)currentHP * 0.001f;
        }

    }
    public void TakeDmg(int dmgAmount)
    {
        currentHP -= dmgAmount;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Damage(int dmgAmount)
    {
        TakeDmg(dmgAmount);
    }


    void Die()
    {
        Debug.Log("Enemy die");
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, _attackRange);
    }
}
