using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathCount : MonoBehaviour,IDataPersistence
{
    public static DeathCount instance;


    public TMP_Text deathCountText;
    public int deathCount = 0;

    private void Awake()
    {
       DeathCount.instance = this;
    }


    public void LoadData(GameData data)
    {
        this.deathCount = data.deathCount;
    }

    public void SaveData(GameData data)
    {
        data.deathCount = this.deathCount;
    }


    private void Start()
    {
        deathCountText.text = "Death: " + deathCount.ToString();
    }

    public void Deathsum(int v)
    {
        deathCount = v;
        deathCountText.text = "Death: " + deathCount.ToString();
    }

}
