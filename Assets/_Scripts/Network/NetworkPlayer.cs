using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Cinemachine;


public class NetworkPlayer : NetworkBehaviour
{
    public static NetworkPlayer Local { get; set; }
    private CinemachineTargetGroup camGroup; 

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;
        }

        camGroup = FindObjectOfType<CinemachineTargetGroup>();
        if (camGroup == null) return;
        camGroup.AddMember(this.gameObject.transform, 1, 1);
    }

    // public void PlayerLeft(PlayerRef player)
    // {
    //     if (player == Object.InputAuthority)
    //     {
    //         Runner.Despawn(Object);
    //     }

    // }
}
