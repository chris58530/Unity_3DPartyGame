using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Crown : NetworkBehaviour
{
    [Networked] private NetworkObject winPlayer { get; set; }
    [SerializeField] private GameObject winCam;
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
        transform.position = winPlayer.transform.position + new Vector3(0, -1f, 0);
        winCam.transform.position = winPlayer.transform.position + new Vector3(0, 1.5f, -4);
    }
}
