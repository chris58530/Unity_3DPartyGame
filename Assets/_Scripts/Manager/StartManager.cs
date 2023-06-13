using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class StartManager : NetworkBehaviour
{
    [SerializeField] private bool dontNeed;
    [SerializeField] private GameObject startCanvas;
    private float time = 3;
    [Networked]
    private TickTimer timer { get; set; }
    public override void Spawned()
    {
        if (dontNeed)
        {
            GameManager.isGame = true;
            return;
        }

        GameManager.isGame = false;

        Runner.Spawn(startCanvas, transform.position, transform.rotation, Object.InputAuthority);
        timer = TickTimer.CreateFromSeconds(Runner, time);

    }
    public override void FixedUpdateNetwork()
    {
        if (dontNeed) return;

        if (timer.Expired(Runner))
        {
            GameManager.isGame = true;
        }
    }
}
