using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTreeActiveEffect : MonoBehaviour
{
    [SerializeField] private float _dissolveTime = 1f;

    private SpriteRenderer _spriteRenderer;
    private Material _material;

    private int _dissolveAmount = Shader.PropertyToID("_DissolveAmount");
    [SerializeField] private bool _canInteract = false;

    [SerializeField] Collider2D col;

    public GameObject leaf;
    public GameObject algae;

    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        _material = _spriteRenderer.material;

        leaf.SetActive(false);
        col = GetComponent<Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision!=null)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                _canInteract = true;
                //col.enabled = false;
            }
        }
    }
    private void Update()
    {
        if(_canInteract && InputManager.Instance.Interact)
        {
            StartCoroutine(Vanish());
            leaf.SetActive(true);
            algae.SetActive(true);
            _canInteract = false;
            col.enabled = false;
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
