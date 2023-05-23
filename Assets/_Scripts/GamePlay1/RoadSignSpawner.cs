using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class RoadSignSpawner : NetworkBehaviour
{
    [SerializeField]
    private Dictionary<NetworkObject,Transform> NetDict = new Dictionary<NetworkObject, Transform>();

}
