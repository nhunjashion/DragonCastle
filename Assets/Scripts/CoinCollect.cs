using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    private SpriteRenderer visual;
    private bool collected =  false;
    public int value = 1;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }


    private void Awake()
    {
        visual = this.GetComponentInChildren<SpriteRenderer>();
    }

    public void LoadData(GameData data)
    {
        data.coinsCollected.TryGetValue(id, out collected);
        if(collected)
        {
            visual.gameObject.SetActive(false);
        }
    }

    public void SaveData(GameData data)
    {
        if (data.coinsCollected.ContainsKey(id))
        {
            data.coinsCollected.Remove(id);
        }
        data.coinsCollected.Add(id, collected);
    }



    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            collected = true;
            visual.gameObject.SetActive(false);
            Debug.Log("coin collected");
            //GameEventsManager.instance.CoinCollected();

            //CoinCounter.instance.IncreaseCoins(value);
        }
    }

}
