using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActive : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool isActive = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }


  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isActive = true;
            rb.isKinematic = false;
        }
    }

  
}
