using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public enum Button{
    JUMP,
    FIRE
}
public struct NetworkInputData : INetworkInput
{
    public NetworkButtons buttons;
    public Vector3 direction;
    
    public float AxisX ;
    public float AxisZ;
    public NetworkBool Move => AxisX != 0 || AxisZ != 0;
}
