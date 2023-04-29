using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


public class NetworkPlayerInput : NetworkBehaviour
{

    public float AxisX => Input.GetAxisRaw("Horizontal");
    public float AxisZ => Input.GetAxisRaw("Vertical");
    public bool Move => AxisX != 0 || AxisZ != 0;
    public bool Jump => Input.GetButton("Jump");
    public bool StopJump => Input.GetButtonUp("Jump");

    public bool Fire => Input.GetButtonDown("Fire1");
    public bool StopFire => !Input.GetButtonDown("Fire1");

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData inputData = new NetworkInputData();
        inputData.AxisX = AxisX;
        inputData.AxisZ = AxisZ;
        inputData.Move = Move;
        inputData.IsFirePressed = Fire;
        inputData.IsJumpPressed = Jump;
        return inputData;
    }
}
