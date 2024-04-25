using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour, IDataPersistence
{
    public static CoinCounter instance;
  

    public TMP_Text coinText;
    public int currentCoins = 0;

    private void Awake()
    {
        CoinCounter.instance = this;
    }

    private void Start()
    {
        coinText.text = "Coins: " + currentCoins.ToString();
    }

    public void LoadData(GameData data)
    {
        foreach (KeyValuePair<string, bool> pair in data.coinsCollected)
        {
            if(pair.Value)
            {
                currentCoins++;
            }
        }
    }

    public void SaveData(GameData data)
    {
        //NO DATA
    }

    public void IncreaseCoins(int v)
    {
        currentCoins += v;
        coinText.text = "Coins: " + currentCoins.ToString();
    }



}
