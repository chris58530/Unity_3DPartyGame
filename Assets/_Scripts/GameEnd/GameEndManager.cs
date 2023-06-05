using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class GameEndManager : NetworkBehaviour
{
    private float EndTime = 10;
    [Networked]
    private TickTimer GameEndTimer { get; set; }



    public override void Spawned()
    {
        GameEndTimer = TickTimer.CreateFromSeconds(Runner, EndTime);
        AudioManager.Instance.RPC_PlaySFX("Cheering");
        foreach (PlayerRef player in GameManager.Instance.PlayerList.Keys)
        {
            if (GameManager.Instance.PlayerList.TryGetValue(player, out NetworkPlayerData data))
            {
                data.PlayerScore = 0;
            }
        }
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
