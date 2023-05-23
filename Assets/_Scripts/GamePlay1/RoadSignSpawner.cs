using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class RoadSignSpawner : NetworkBehaviour
{
    [SerializeField]
    private NetworkPrefabRef grass;
    [SerializeField]
    private NetworkPrefabRef sign;
    [SerializeField]
    private NetworkPrefabRef railing;
    [SerializeField]
    private Transform signTrans;
    [SerializeField]
    private Transform grassTrans;
    [SerializeField]
    private NetworkPrefabRef railingTrans;

}
