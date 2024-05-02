using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public Animator anim;
    public GameObject slashes;

    bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        DisableSlashes();
    }

    // Update is called once per frame
    void Update()
    {
        DisableSlashes();
        if(InputManager.Instance.OnFiring && !attacking)
        {
            attacking = true;
            anim.SetTrigger("Attack");
            StartCoroutine(SlashAttack());
        }

    }


    IEnumerator SlashAttack()
    {
        yield return new WaitForSeconds(.1f);
        slashes.SetActive(true);       
    }

    void DisableSlashes()
    {
        slashes.SetActive(false);
    }
}
