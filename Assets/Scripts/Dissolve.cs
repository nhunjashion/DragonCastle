using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] private float _dissolveTime = 0.75f;

    [SerializeField] private SpriteRenderer[] _srs;
    [SerializeField] private Material[] _mats;

    [SerializeField] private int _dissolveAmount = Shader.PropertyToID("DissolveAmount");

    private void Start()
    {


    }

}
