using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityActive : MonoBehaviour
{
    PlayerBash Bash;
    private bool _isBashActive =  false;
    [SerializeField] protected GameObject bashTreeAbility;

    private void Start()
    {
        Bash = gameObject.GetComponent<PlayerBash>();
        Bash.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.CompareTag("AbilityTree"))
            {
                _isBashActive = true;

                Debug.Log("Bash skill unlocked");
            }
        }
    }


    private void Update()
    {
        ActiveBash();
    }

    void ActiveBash()
    {
        if(_isBashActive)
        {
            Bash.enabled = true;
            bashTreeAbility.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
