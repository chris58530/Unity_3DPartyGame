using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public struct NetworkInputData : INetworkInput
{
    public NetworkBool IsJumpPressed;
    public NetworkBool IsFirePressed;
    public Vector3 direction;
    
    public float AxisX ;
    public float AxisZ;
    public NetworkBool Move;
    
}