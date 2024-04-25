using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private float _knockBackTime = 0.2f;
    [SerializeField] protected float hitDirectionForce = 10f;
    public float consForce = 5f;
    public float inputForce = 7.5f;
    
    public bool isBeingKnockBack { get; private set; }

    Rigidbody2D rb;


    private Coroutine knockbackCoroutine;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public IEnumerator KnockBackAction(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
    {
        isBeingKnockBack = true;

        Vector2 _hitForce;
        Vector2 _constantForce;
        Vector2 _knockbackForce;
        Vector2 _combineForce;


        _hitForce = hitDirection * hitDirectionForce;
        _constantForce = constantForceDirection * consForce;

        float _elapsedTime = 0f;
        while(_elapsedTime < _knockBackTime)
        {
            _elapsedTime += Time.fixedDeltaTime;
            
            _knockbackForce = _hitForce + _constantForce;

            if(inputDirection != 0)
            {
                _combineForce = _knockbackForce + new Vector2(inputDirection, 0f);
            }
            else
            {
                _combineForce = _knockbackForce;
            }

            rb.velocity = _combineForce;

            yield return new WaitForFixedUpdate();
        }

        isBeingKnockBack = false;
    }


    public void CallKnockBack(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
    {
        knockbackCoroutine = StartCoroutine(KnockBackAction(hitDirection, constantForceDirection, inputDirection));
    }
}
