using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafColorTransition : MonoBehaviour
{
    public Color activeColor;
    public SpriteRenderer sr;
    private float _time;

    private void Update()
    {
        Transition();
        sr = GetComponent<SpriteRenderer>();
    }

    void Transition()
    {
        _time += Time.deltaTime;
        sr.color = Color.Lerp(sr.color, activeColor, _time);

    }
}
