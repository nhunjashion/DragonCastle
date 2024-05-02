using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTreeActiveEffect : MonoBehaviour
{
    [SerializeField] private float _dissolveTime = 1f;

    private SpriteRenderer _spriteRenderer;
    private Material _material;

    private int _dissolveAmount = Shader.PropertyToID("_DissolveAmount");

    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        _material = _spriteRenderer.material;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(Vanish());
        }
    }
    private IEnumerator Vanish()
    {
        float elapsedTime = 0f;
        while(elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedDissolve = Mathf.Lerp(0, 1.1f,(elapsedTime/_dissolveTime));

            _material.SetFloat(_dissolveAmount,lerpedDissolve);

            yield return null;
        }

    }
}
