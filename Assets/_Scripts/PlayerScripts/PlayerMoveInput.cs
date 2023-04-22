using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerMoveInput : MonoBehaviour
{


    // public Vector2 moveInput { get; private set; }
    public float speedtime;
    Vector2 axes => map.FindAction("Axes").ReadValue<Vector2>();
    public float AxisX => axes.x;
    public float AxisZ => axes.y;
    public bool Move => AxisX != 0 || AxisZ != 0;
    public bool Jump => map.FindAction("Jump").WasPressedThisFrame();
    public bool StopJump => map.FindAction("Jump").WasReleasedThisFrame();
    public bool positiveArrow => map.FindAction("Positive").IsPressed();
    public bool nagativeArrow => map.FindAction("Negative").IsPressed();
    public bool Throw => map.FindAction("Throw").IsPressed();
    public bool StopThrow => map.FindAction("Throw").WasReleasedThisFrame();
    [SerializeField]
    private TMP_Text speedText;

    [SerializeField]
    InputActionAsset asset;
    InputActionMap map;
    [SerializeField]
    private string mapName;

    void Awake()
    {
        map = asset.FindActionMap(mapName);
        speedText.enabled = false;

    }

    void Update()
    {
        if (Move)
        {
            speedtime += Time.deltaTime;
        }
        else
        {
            ShowRushSpeed(false);
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
    public void ShowRushSpeed(bool active)
    {
        speedText.enabled = active;
        speedText.text = Mathf.Round(speedtime).ToString();
    }
    public void EnableGamePlayInputs()
    {
        asset.FindActionMap(mapName).Enable();
    }

}
