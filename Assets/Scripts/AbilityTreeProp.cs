using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTreeProp : MonoBehaviour
{
    public static AbilityTreeProp instance;
    public int treeNum;
    private bool _isAbilityCanActive = false;
    public bool isAbilityActive = false;
    public bool isDashUnlock = false;

    [SerializeField] protected GameObject treeAbility;
    [SerializeField] protected GameObject player;


    Collider2D col;
    PlayerBash bash;
    PlayerDash dash;
    SwordSlash swordSlash;

    public GameObject skillIcon;
    public GameObject skillPopup;

    void Start()
    {
        AbilityTreeProp.instance = this;
        col = treeAbility.GetComponent<Collider2D>();
        bash = player.gameObject.GetComponent<PlayerBash>();
        dash = player.gameObject.GetComponent<PlayerDash>();
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

    public void AbilityUnlock()
    {

        if(_isAbilityCanActive && InputManager.Instance.Interact)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.aBTreeActive);
            skillIcon.SetActive(true);

            if (treeNum == 1)
            {
                bash.enabled = true;
                
                skillPopup.SetActive(true);
                Debug.Log("skill unlocked");

            }
            else if (treeNum == 2)
            
            {
                dash.enabled = true;
                skillPopup.SetActive(true);
                isDashUnlock = true;
                Debug.Log("skill unlocked");
            }else if (treeNum == 3)
            {
                swordSlash.enabled = true;
                skillPopup.SetActive(true);
                Debug.Log("skill unlocked");
            }
            col.enabled = false;
            //AbilityActive.instance.ActiveSkill(treeNum);
            Debug.Log("interact ");
        }
    }

    public void HidePopup()
    {
        Destroy(skillPopup);
    }
}
