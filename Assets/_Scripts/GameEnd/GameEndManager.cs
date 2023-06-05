using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class GameEndManager : NetworkBehaviour
{
    [SerializeField]
    private float EndTime = 10;
    [Networked]
    private TickTimer GameEndTimer { get; set; }

   

    public override void Spawned()
    {
        GameEndTimer = TickTimer.CreateFromSeconds(Runner, EndTime);
       AudioManager.Instance.RPC_PlaySFX("Cheering");

    }

    public override void FixedUpdateNetwork()
    {
        
        if (GameEndTimer.Expired(Runner))
        {
            Debug.Log("gameend switch scene");
            GameManager.Instance.NextScene();
        }

    }
    
}
