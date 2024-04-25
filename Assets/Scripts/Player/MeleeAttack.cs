using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    
    
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected float meleeAttackRange = 0.5f;
    [SerializeField] protected LayerMask enemyLayers;
    [SerializeField] protected int baseDmg = 4;


    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        MeleeAttacking();
    }

    void MeleeAttacking()
    {


        if (InputManager.Instance.OnFiring)
        {
            PlayerMovement.Instance.FlipAnim();
            anim.SetTrigger("Attack");

            Collider2D[] hitEnemies =  Physics2D.OverlapCircleAll(attackPoint.position, meleeAttackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("We hit " + enemy.name);
                EnemyCtrl.Instance.TakeDmg(baseDmg);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, meleeAttackRange);
    }

}
