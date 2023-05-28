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

    public bool Move;
    public bool Jump;
    public bool StopJump;
    public bool Open;
    public bool StopOpen;
    public bool Shoot;
    public bool IsGround;
    public void Update()
    {
        AxisX = Input.GetAxisRaw("Horizontal");
        AxisZ = Input.GetAxisRaw("Vertical");
        Move = AxisX != 0 || AxisZ != 0;
        Jump = Input.GetButton("Jump");
        StopJump = !Input.GetButton("Jump");
        Open = Input.GetButton("Fire1")||Input.GetKey(KeyCode.K);
        StopOpen = !Input.GetButton("Fire1");
        Shoot =Input.GetButton("Fire2");
    
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData inputData = new NetworkInputData();
        inputData.AxisX = AxisX;
        inputData.AxisZ = AxisZ;
        inputData.Move = Move;

        inputData.IsOpenPressed = Open;
        inputData.IsJumpPressed = Jump;
        inputData.IsShootPressed = Shoot;
        inputData.StopOpen = StopOpen;
        return inputData;
    }
}
