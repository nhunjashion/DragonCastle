using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerHealth : MonoBehaviour, IDamageable, IDataPersistence
{
    protected static PlayerHealth instance;
    public static PlayerHealth Instance { get => instance; }


    private KnockBack knockback;

    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth = 100;
    public int Hp { get => currentHealth; }

    [SerializeField] private Slider hpBar;

    Animator anim;
    public GameObject player;
    PlayerDash dash;

    public bool canGetDmg = true;

    //respawn
    private int _deathCount = 0;
    [SerializeField]SpriteRenderer sr;
    [SerializeField]Rigidbody2D rb;
    [SerializeField] private Transform respawnPoint;

    public bool isInv;

    public void LoadData(GameData data)
    {
        this._deathCount = data.deathCount;
    }

    public void SaveData(GameData data)
    {
        data.deathCount = this._deathCount;
    }




    void Start()
    {

        PlayerHealth.instance = this;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        knockback = GetComponent<KnockBack>();
        dash = player.gameObject.GetComponent<PlayerDash>();
    }

    private void Update()
    {
        SetHPValue();
    }

    public void SetHPValue()
    {
        if(hpBar.value != currentHealth)
        {
            hpBar.value = (float)currentHealth*0.01f;
        }

    }


    void GetDmg(int dmgAmount)
    {


        if(canGetDmg)
        {
            currentHealth -= dmgAmount;
        }




        if(currentHealth <= 0)
        {
            Die();
        }
       // knockback.CallKnockBack(hitDirection, Vector2.up, InputManager.Instance.Horizontal);
    }




    public void Damage(int dmgAmount)
    {
        GetDmg(dmgAmount);
    }

    public void Die()
    {
        _deathCount++;
        _ = StartCoroutine(Respawning(1f));
    }

    IEnumerator Respawning(float duration)
    {
        anim.SetBool("IsDead",true);
        yield return new WaitForSeconds(duration);
        rb.simulated = false;
        yield return new WaitForSeconds(duration);

        sr.enabled = false;
        yield return new WaitForSeconds(duration);

        transform.position = respawnPoint.position;
        currentHealth = maxHealth;
        sr.enabled = true;
        rb.simulated = true;
        anim.SetBool("IsDead",false);  
        Debug.Log("RESPAWN");
    }
}
