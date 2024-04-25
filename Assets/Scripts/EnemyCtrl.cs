using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    protected static EnemyCtrl instance;
    public static EnemyCtrl Instance { get => instance; }

    [SerializeField] private int _maxHP = 10;
    [SerializeField] protected int currentHP;

    Animator anim;
    //Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = _maxHP;
        EnemyCtrl.instance = this;

        //rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    public void TakeDmg(int dmg)
    {
        currentHP -= dmg;

        if(currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //rb.gravityScale = 0f;
        Debug.Log("Enemy die");
        anim.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
    }

    
}
