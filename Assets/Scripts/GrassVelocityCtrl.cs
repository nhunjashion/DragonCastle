using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassVelocityCtrl : MonoBehaviour
{
    [Range(0f, 1f)] public float ExternalInFluenceStrength = 0.25f;
    public float EaseInTime = 0.15f;
    public float EaseOutTime = 0.15f;

    public float VelocityThreshold = 5f;

    private int _externalInfluence = Shader.PropertyToID("_ExternalInfluence");

    public void InfluenceGrass(Material mat, float XVelocity)
    {
        mat.SetFloat(_externalInfluence, XVelocity);
    }
}
