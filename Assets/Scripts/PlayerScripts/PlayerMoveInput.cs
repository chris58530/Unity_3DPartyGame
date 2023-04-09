using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveInput : MonoBehaviour
{


    // public Vector2 moveInput { get; private set; }
    public float speedtime { get; private set; }
    Vector2 axes => playerInputAction.GamePlay.Axes.ReadValue<Vector2>();
    public float AxisX => axes.x;
    public float AxisZ => axes.y;
    public bool Move => AxisX != 0 || AxisZ != 0;
    public bool Jump => playerInputAction.GamePlay.Jump.WasPressedThisFrame();
    public bool StopJump => playerInputAction.GamePlay.Jump.WasReleasedThisFrame();
    public bool positiveArrow => playerInputAction.GamePlay.Positive.IsPressed();
    public bool nagativeArrow => playerInputAction.GamePlay.Negative.IsPressed();
    
    PlayerInputAction playerInputAction;
    // [SerializeField]
    // InputActionAsset dsd;


    GameObject magentDetector;
    // float v, h = 0;
    void Awake()
    {
        playerInputAction = new PlayerInputAction();
        // dsd.FindActionMap("asas").Enable();
    }
    void OnEnable()
    {
        magentDetector = transform.GetComponentInChildren<MagnetBody>().gameObject;
    }
    void Update()
    {

        // v = Input.GetKey(forwardArrow) ? 1 : (Input.GetKey(backArrow) ? -1 : 0);
        // h = Input.GetKey(rightArrow) ? 1 : (Input.GetKey(leftArrow) ? -1 : 0);
        if (Move)
        {
            speedtime += Time.deltaTime;
        }
        else
        {
            if (speedtime >= 0)
            {
                speedtime -= Time.deltaTime * 50;
            }
            else
            {
                speedtime = 0;
            }
        }
        // moveInput = new Vector2(v, h);
        MagentChange();

    }
    public void EnableGamePlayInputs()
    {
        playerInputAction.GamePlay.Enable();


    }
    void MagentChange()
    {
        if (positiveArrow)
        {
            magentDetector.tag = "Positive";
        }
        else if (nagativeArrow)
        {
            magentDetector.tag = "Negative";
        }
        else
        {

            magentDetector.tag = "None";
        }
    }
}
