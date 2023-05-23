using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class GameEndManager : NetworkBehaviour
{
    [SerializeField]
    private float EndTime = 5;
    [Networked]
    private TickTimer GameEndTimer { get; set; }

    public override void Spawned()
    {
        TickTimer.CreateFromSeconds(Runner, 3);
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
        EndTime-= Runner.DeltaTime;
        if (EndTime <= 0)
        {
            Debug.Log("gameend switch scene");
            GameManager.Instance.NextScene();
        }

    }
}
