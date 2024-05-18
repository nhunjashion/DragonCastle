using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int deathCount;
    public Vector3 playerPosition;
    public Vector3 playerRespawnPoint;
    public SerializableDictionary<string, bool> coinsCollected;

    public GameData()
    {
        deathCount = 0;
        playerPosition = Vector3.zero;
        playerRespawnPoint = Vector3.zero;
        coinsCollected = new SerializableDictionary<string, bool>();
    }
}
