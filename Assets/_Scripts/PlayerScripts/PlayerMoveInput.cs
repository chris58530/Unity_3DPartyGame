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
    public bool Jump =>map.FindAction("Jump").WasPressedThisFrame();
    public bool StopJump => map.FindAction("Jump").WasReleasedThisFrame();
    public bool positiveArrow => map.FindAction("Positive").IsPressed();
    public bool nagativeArrow => map.FindAction("Negative").IsPressed();
    public bool Throw => map.FindAction("Throw").IsPressed();
    public bool StopThrow => map.FindAction("Throw").WasReleasedThisFrame();


    [SerializeField]
    InputActionAsset asset;
    InputActionMap map;
    [SerializeField]
    private string mapName;
  
    void Awake()
    {
        map = asset.FindActionMap(mapName);
    }
    void OnEnable()
    {


    }

    void Update()
    {

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

    }
    public void EnableGamePlayInputs()
    {
        asset.FindActionMap(mapName).Enable();

    }

}