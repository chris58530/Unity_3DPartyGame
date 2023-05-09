using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public struct NetworkInputData : INetworkInput
{
    public NetworkBool IsJumpPressed;
    public NetworkBool IsFirePressed;
    public NetworkBool StopJump;

    public NetworkBool StopFire;
    public Vector3 direction;
    
    public float AxisX ;
    public float AxisZ;
    public float SpeedTime;
    public NetworkBool Move;
    
}
