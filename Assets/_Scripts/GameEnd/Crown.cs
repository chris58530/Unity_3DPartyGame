using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Crown : NetworkBehaviour
{
    private NetworkObject winPlayer;
    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            foreach (PlayerRef player in GameManager.Instance.PlayerList.Keys)
            {
                if (GameManager.Instance.PlayerList.TryGetValue(player, out NetworkPlayerData data))
                {
                    if (data.PlayerScore >= 3)
                    {
                        NetworkObject win = GameManager.Instance.Runner.GetPlayerObject(player);
                        winPlayer = win;
                    }
                }
            }
        }
    }
    public override void FixedUpdateNetwork()
    {
        transform.position = winPlayer.transform.position + new Vector3(0,-1f,0);

    }
}
