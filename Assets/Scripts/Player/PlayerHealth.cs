using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private KnockBack knockback;

    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    
    void Start()
    {
        currentHealth = maxHealth;
        knockback = GetComponent<KnockBack>();
    }


    public void Damage(float dmgAmount, Vector2 hitDirection)
    {
        currentHealth -= dmgAmount;

        knockback.CallKnockBack(hitDirection, Vector2.up, InputManager.Instance.Horizontal);
    }
}
