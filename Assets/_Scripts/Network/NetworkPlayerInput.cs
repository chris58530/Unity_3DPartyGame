using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


public class NetworkPlayerInput : NetworkBehaviour
{

    // public float AxisX => Input.GetAxisRaw("Horizontal");
    // public float AxisZ => Input.GetAxisRaw("Vertical");
    // public bool Move => AxisX != 0 || AxisZ != 0;
    public float AxisX;
    public float AxisZ;
    public float SpeedTime;
    public bool Move;
    public bool Jump;
    public bool StopJump;

    public bool Fire;
    public bool StopFire;
    public void Update()
    {
        AxisX = Input.GetAxisRaw("Horizontal");
        AxisZ = Input.GetAxisRaw("Vertical");
        Move = AxisX != 0 || AxisZ != 0;
        Jump = Input.GetButton("Jump");
        StopJump = !Input.GetButton("Jump");
        Fire = Input.GetButtonDown("Fire1");
        StopFire = !Input.GetButtonDown("Fire1");
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData inputData = new NetworkInputData();
        inputData.AxisX = AxisX;
        inputData.AxisZ = AxisZ;
        inputData.Move = Move;
        inputData.SpeedTime = SpeedTime;
        inputData.IsFirePressed = Fire;
        inputData.IsJumpPressed = Jump;
        return inputData;
    }
}
