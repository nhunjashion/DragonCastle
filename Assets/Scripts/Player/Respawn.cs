using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour, IDataPersistence
{
    public static Respawn instance;
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

    public void Die()
    {
        if(PlayerHealth.Instance.Hp <=0)
        {
            _deathCount++;
            StartCoroutine(Respawning(1f));
            Debug.Log("RESPAWN");
        }

    }

    IEnumerator Respawning(float duration)
    {
        anim.SetTrigger("IsDead");
        rb.simulated = false;
        yield return new WaitForSeconds(duration);

        sr.enabled = false;
        yield return new WaitForSeconds(duration);

        transform.position = respawnPoint.position;
        sr.enabled = true;
        rb.simulated = true;
        anim.SetTrigger("IsDead");
    }
}
