using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputForEnemy : MonoBehaviour
{
    protected static InputForEnemy instance;
    public static InputForEnemy Instance { get => instance; }


    private Vector3 _playerPos;
    public Vector3 PlayerPos {  get => _playerPos; }


    private void Awake()
    {
        InputForEnemy.instance = this;
 
    }

    private void Update()
    {
        GetPlayerPos();
    }

    void GetPlayerPos()
    {
       _playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
