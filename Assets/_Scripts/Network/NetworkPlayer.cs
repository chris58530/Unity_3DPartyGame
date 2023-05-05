using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkPlayer : NetworkBehaviour
{
    public static NetworkPlayer Local { get; set; }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;
        }
    }

    // public void PlayerLeft(PlayerRef player)
    // {
    //     if (player == Object.InputAuthority)
    //     {
    //         Runner.Despawn(Object);
    //     }

    // }
}
