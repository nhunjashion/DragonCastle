using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBash : MonoBehaviour
{

    [Header("Bash")]
    [SerializeField] protected float raduis;
    [SerializeField] protected GameObject BashAbleObj;
    private bool _nearToBashAbleObj;
    private bool _isChosingDir;
    private bool _isBashing;
    [SerializeField] private float _bashPower;
    [SerializeField] private float _bashTime;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject player;
    Vector3 bashDir;
    private float bashTimeReset;

    Rigidbody2D rb;

    Animator anim;

    public CapsuleCollider2D col;
    public float timeInv = 0.1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bashTimeReset = _bashTime;
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider2D>();
    }

   
    private void Update()
    {
        Bashing();
    }


    void Bashing()
    {

        RaycastHit2D[] Rays = Physics2D.CircleCastAll(transform.position, raduis, Vector3.forward);
        foreach (RaycastHit2D ray in Rays)
        {
           // anim.SetTrigger("IsBashing");
            _nearToBashAbleObj = false;
            if(ray.collider.tag == "BashAble")
            {
                _nearToBashAbleObj = true;
                BashAbleObj = ray.collider.transform.gameObject;
                break;
            }
        }
        if (_nearToBashAbleObj)
        {
            BashAbleObj.GetComponent<SpriteRenderer>().color = Color.cyan;
            if (Input.GetKeyDown(KeyCode.Q)) 
            {

                Time.timeScale = 0;
                BashAbleObj.transform.localScale = new Vector2(1.4f, 1.4f);
                arrow.SetActive(true);
                arrow.transform.position = BashAbleObj.transform.transform.position;
                _isChosingDir = true;
            }
            else if (_isChosingDir && Input.GetKeyUp(KeyCode.Q))
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.bash);
                Time.timeScale = 1;
                BashAbleObj.transform.localScale = new Vector2(1, 1);
                _isChosingDir = false;
                _isBashing = true;
                rb.velocity = Vector2.zero;

                transform.position = BashAbleObj.transform.position;
                bashDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                bashDir.z = 0;
                if (bashDir.x > 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                bashDir = bashDir.normalized;
                BashAbleObj.GetComponent<Rigidbody2D>().AddForce(-bashDir * 50, ForceMode2D.Impulse);
                arrow.SetActive(false);
            }
            //transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (BashAbleObj != null)
        {
            BashAbleObj.GetComponent<SpriteRenderer>().color = Color.white;
        }



        if (_isBashing)
        {

            if (_bashTime > 0)
            {

                PlayerHealth.Instance.canGetDmg = false;
                _bashTime -= Time.deltaTime;
                rb.velocity = bashDir * _bashPower * Time.deltaTime;

            }
            else
            {
                PlayerHealth.Instance.canGetDmg = true;
                timeInv = 0.1f;
                _isBashing = false;
                _bashTime = bashTimeReset;
                rb.velocity = new Vector2(rb.velocity.x, 0);

            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, raduis);  
    }
}
