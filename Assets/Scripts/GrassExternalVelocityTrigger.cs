using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrassExternalVelocityTrigger : MonoBehaviour
{
    private GrassVelocityCtrl _grassVelocityCtrl;

    private GameObject _player;

    private Material _material;

    Rigidbody2D rb;

    private bool _easeInCoroutineRunning;
    private bool _easeOutCoroutineRunning;

    private int _externalInfluence = Shader.PropertyToID("_ExternalInfluence");

    private float _startingXVelocity;
    private float _velocityLastFrame;

    private void Start()
    {

        _player = GameObject.FindGameObjectWithTag("Player");
        rb= _player.GetComponent<Rigidbody2D>();
        _grassVelocityCtrl = GetComponentInParent<GrassVelocityCtrl>();

        _material = GetComponent<SpriteRenderer>().material;
        _startingXVelocity = _material.GetFloat(_externalInfluence);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == _player)
        {
            if(!_easeInCoroutineRunning && Mathf.Abs(rb.velocity.x)>Mathf.Abs(_grassVelocityCtrl.VelocityThreshold))
            {
                StartCoroutine(EaseIn(rb.velocity.x * _grassVelocityCtrl.ExternalInFluenceStrength));
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject== _player)
        {
            StartCoroutine(EaseOut());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject == _player)
        {
            if(Mathf.Abs(_velocityLastFrame)>Mathf.Abs(_grassVelocityCtrl.VelocityThreshold) &&
                Mathf.Abs(rb.velocity.x) < Mathf.Abs(_grassVelocityCtrl.VelocityThreshold))
            {
                StartCoroutine(EaseOut());
            }
            else if(Mathf.Abs(_velocityLastFrame)<Mathf.Abs(_grassVelocityCtrl.VelocityThreshold) &&
                Mathf.Abs(rb.velocity.x)>Mathf.Abs(_grassVelocityCtrl.VelocityThreshold))
            {
                StartCoroutine(EaseIn(rb.velocity.x * _grassVelocityCtrl.ExternalInFluenceStrength));
            }

            _velocityLastFrame = rb.velocity.x;
        }
        
    }



    private IEnumerator EaseIn(float XVelocity)
    {
        _easeInCoroutineRunning = true;

        float elapsedTime = 0f;
        while(elapsedTime < _grassVelocityCtrl.EaseInTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedAmount = Mathf.Lerp(_startingXVelocity, XVelocity, (elapsedTime / _grassVelocityCtrl.EaseInTime));
            _grassVelocityCtrl.InfluenceGrass(_material, lerpedAmount);

            yield return null;
        }

        _easeInCoroutineRunning = false;
    }

    private IEnumerator EaseOut()
    {
        _easeOutCoroutineRunning = true;
        float currentXInfluence = _material.GetFloat(_externalInfluence);

        float elapsedTime = 0f;
        while(elapsedTime < _grassVelocityCtrl.EaseOutTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedAmount = Mathf.Lerp(currentXInfluence, _startingXVelocity, (elapsedTime / _grassVelocityCtrl.EaseOutTime));
            _grassVelocityCtrl.InfluenceGrass(_material, lerpedAmount);

            yield return null;
        }

        _easeOutCoroutineRunning = false;
    }
}
