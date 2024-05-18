using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityActive : MonoBehaviour
{
    public static AbilityActive instance;
    PlayerBash bash;
    PlayerDashFlex dash;
    SwordSlash swordSlash;
    int num;

    [SerializeField] protected GameObject player;

    private void Start()
    {
        bash = player.gameObject.GetComponent<PlayerBash>();
        dash = player.gameObject.GetComponent<PlayerDashFlex>();
        swordSlash = player.gameObject.GetComponent<SwordSlash>();
        num = AbilityTreeProp.instance.treeNum;
    }

    public void ActiveSkill(int num)
    {
        if (num ==1)
        {
                    bash.enabled = true;

        Debug.Log("skill unlocked");

        }

        if (num ==2)
        {
            dash.enabled = true;
            Debug.Log("skill unlocked");
        }
        if (num == 3)
        {
            swordSlash.enabled = true;
            Debug.Log("skill unlocked");
        }
    }
}
