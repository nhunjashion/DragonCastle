using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour, IDamageable
{
    protected static EnemyCtrl instance;
    public static EnemyCtrl Instance { get => instance; }

    [SerializeField] private int _maxHP = 10;
    [SerializeField] public int currentHP;

    Animator anim;

    public ParticleSystem dieEff;
    //Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = _maxHP;
        EnemyCtrl.instance = this;

        //rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    public void TakeDmg(int dmgAmount)
    {
        currentHP -= dmgAmount;

        if(currentHP <= 0)
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
        //rb.gravityScale = 0f;
        Debug.Log("Enemy die");
        anim.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        dieEff.Play();
        Destroy(gameObject);
    }

    
}
