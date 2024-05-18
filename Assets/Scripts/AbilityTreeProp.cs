using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTreeProp : MonoBehaviour
{
    public static AbilityTreeProp instance;
    public int treeNum;
    private bool _isAbilityCanActive = false;
    public bool isAbilityActive = false;

    [SerializeField] protected GameObject treeAbility;
    [SerializeField] protected GameObject player;


    Collider2D col;
    PlayerBash bash;
    PlayerDashFlex dash;
    SwordSlash swordSlash;

    void Start()
    {
        col = treeAbility.GetComponent<Collider2D>();
        bash = player.gameObject.GetComponent<PlayerBash>();
        dash = player.gameObject.GetComponent<PlayerDashFlex>();
        swordSlash = player.gameObject.GetComponent<SwordSlash>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _isAbilityCanActive = true;

                Debug.Log("skill unlocked");
            }
        }
    }

    void Update()
    {
        AbilityUnlock();
    }

    void AbilityUnlock()
    {

        if(_isAbilityCanActive && InputManager.Instance.Interact)
        {
            if (treeNum == 1)
            {
                bash.enabled = true;

                Debug.Log("skill unlocked");

            }
            else if (treeNum == 2)
            
            {
                dash.enabled = true;
                Debug.Log("skill unlocked");
            }else if (treeNum == 3)
            {
                swordSlash.enabled = true;
                Debug.Log("skill unlocked");
            }
            col.enabled = false;
            //AbilityActive.instance.ActiveSkill(treeNum);
            Debug.Log("interact ");
        }
    }
}
