using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActive : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool isActive = false;

    float timer = 0f;
    [SerializeField] private float timeRespawn = 4f;
    [SerializeField] private GameObject respawnPoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    private void Update()
    {
        ReSpawn();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isActive = true;
            rb.isKinematic = false;
        }
    }

    private void ReSpawn()
    {

        if(isActive)
        {
            timer += Time.deltaTime;
            if(timer >= timeRespawn)
            {
                rb.position = respawnPoint.transform.position;
                timer = 0f;
            }
        }
    }
  
}
