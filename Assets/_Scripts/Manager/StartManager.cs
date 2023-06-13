using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class StartManager : NetworkBehaviour
{
    [SerializeField] private GameObject startCanvas;
    private float time = 3;
    [Networked]
    private TickTimer timer { get; set; }
    public override void Spawned()
    {
        GameManager.isGame = false;

        Runner.Spawn(startCanvas, transform.position, transform.rotation, Object.InputAuthority);
        timer = TickTimer.CreateFromSeconds(Runner, time);
    }
    public override void FixedUpdateNetwork()
    {
        if (timer.Expired(Runner))
        {
            GameManager.isGame = true;
        }
    }
}
