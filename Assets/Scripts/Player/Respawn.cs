using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour, IDataPersistence
{
    [SerializeField] private Transform respawnPoint;
    
    private int _deathCount=0;
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();    
    }

    /**/
    public void LoadData(GameData data)
    {
        this._deathCount = data.deathCount;
    }

    public void SaveData(GameData data)
    {
        data.deathCount = this._deathCount;
    }



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Harmful"))
        {
            Die();
            _deathCount++;
            //DeathCount.instance.Deathsum(_deathCount);
        }
    }


    void Die()
    {
        StartCoroutine(Respawning(1f));
    }

    IEnumerator Respawning(float duration)
    {
        anim.SetBool("IsDead", true);
        rb.simulated = false;
        yield return new WaitForSeconds(duration);

        sr.enabled = false;
        yield return new WaitForSeconds(duration);

        transform.position = respawnPoint.position;
        sr.enabled = true;
        rb.simulated = true;
        anim.SetBool("IsDead", false);
        
    }
}
