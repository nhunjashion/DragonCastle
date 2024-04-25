using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCtrl : MonoBehaviour
{
    public Camera cam;
    public LineRenderer lineRenderer;
    public Transform firePoint;
    private bool _isShootingBeam;
    [SerializeField] private LayerMask _layerMask;

    private void Start()
    {
        DisableLaser();
    }

    private void Update()
    {
        EnableLaser();
        UpdateLaser();
        DisableLaser();
    }


    private void EnableLaser()
    {
        if(Input.GetButtonDown("Fire2"))
        {
           
            lineRenderer.enabled = true;

        }
    }

    void UpdateLaser()
    {
        if(Input.GetButton("Fire2"))
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, InputManager.Instance.MousePos);
        }

        /**/

        Vector2 direction = (Vector2)InputManager.Instance.MousePos - (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction.normalized, direction.magnitude, _layerMask);

        if(hit)
        {
            lineRenderer.SetPosition(1, hit.point);
        }
        if(hit.collider != null)
        {
            //Debug.Log("collide with something");
        }
    }



    private void DisableLaser()
    {
        if(Input.GetButtonUp("Fire2"))
        {
            lineRenderer.enabled = false;
        }
        
    }

   
}
