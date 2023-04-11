using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveInput : MonoBehaviour
{


    // public Vector2 moveInput { get; private set; }
    public float speedtime { get; private set; }
    Vector2 axes => map.FindAction("Axes").ReadValue<Vector2>();
    public float AxisX => axes.x;
    public float AxisZ => axes.y;
    public bool Move => AxisX != 0 || AxisZ != 0;
    // public bool Jump => playerInputAction.GamePlay.Jump.WasPressedThisFrame();
    // public bool StopJump => playerInputAction.GamePlay.Jump.WasReleasedThisFrame();
    // public bool positiveArrow => playerInputAction.GamePlay.Positive.IsPressed();
    // public bool nagativeArrow => playerInputAction.GamePlay.Negative.IsPressed();
    public bool Jump =>map.FindAction("Jump").WasPressedThisFrame();
    public bool StopJump => map.FindAction("Jump").WasReleasedThisFrame();
    public bool positiveArrow => map.FindAction("Positive").IsPressed();
    public bool nagativeArrow => map.FindAction("Negative").IsPressed();

    // PlayerInputAction playerInputAction;

    [SerializeField]
    InputActionAsset asset;
    InputActionMap map;
    [SerializeField]
    private string mapName;
  
    void Awake()
    {
        // playerInputAction = new PlayerInputAction();
        map = asset.FindActionMap(mapName);
    }
    void OnEnable()
    {


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

    }
    public void EnableGamePlayInputs()
    {
        // playerInputAction.GamePlay.Enable();
        asset.FindActionMap(mapName).Enable();

    }

}
