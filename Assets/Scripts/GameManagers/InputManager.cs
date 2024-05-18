using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    protected static InputManager instance;
    public static InputManager Instance { get => instance; }

    [SerializeField] protected Vector3 mousePos;
    public Vector3 MousePos { get => mousePos; }

    [SerializeField] protected float horizontal;
    public float Horizontal { get => horizontal; }

    [SerializeField] protected bool onFiring;
    public bool OnFiring { get => onFiring; }

    [SerializeField] protected bool jump;
    public bool Jump { get => jump; }

    [SerializeField] protected bool dashing;
    public bool Dashing { get => dashing; }

    [SerializeField] protected bool rangedAttack;
    public bool RangedAttack { get => rangedAttack; }

    [SerializeField] protected bool shootBeam;
    public bool ShootBeam { get => shootBeam; }

    [SerializeField] protected bool pausePress;
    public bool PausePress { get => pausePress; }

    [SerializeField] protected bool up;
    public bool Up { get => up; }


    [SerializeField] protected bool interact;
    public bool Interact { get => interact; }



    private void Awake()
    {
        InputManager.instance = this;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GetMousePos();
        GetDirection();
        GetMouseClick();
        GetSpace();
        GetDash();
        GetRangedAttack();
        GetShootBeam();
        GetEscButton();
        GetUp();
        GetInteract();
    }

    protected virtual void GetMousePos()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void GetDirection()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    protected virtual bool GetMouseClick()
    {
        onFiring = Input.GetKeyDown(KeyCode.Mouse0);
        return onFiring;
    }



    public void GetSpace()
    {
        jump = Input.GetKeyDown(KeyCode.Space);
    }

    protected virtual void GetDash()
    {
        dashing = Input.GetKeyDown(KeyCode.E);
    }


    protected virtual void GetRangedAttack()
    {
        rangedAttack = Input.GetKeyDown(KeyCode.Q);
    }

    protected virtual void GetShootBeam()
    {
        shootBeam = Input.GetKeyDown(KeyCode.R);
    }


    protected virtual void GetEscButton()
    {
        pausePress = Input.GetKeyDown(KeyCode.Escape);
    }

    protected virtual void GetUp()
    {
        up = Input.GetKeyDown(KeyCode.W);
    }


    protected virtual void GetInteract()
    {
        interact = Input.GetKeyDown(KeyCode.F);
    }
}
