using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Train : NetworkBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float lifeTime;

    [Networked]
    private TickTimer trainlifeTimer { get; set; }
    public override void Spawned()
    {
        trainlifeTimer = TickTimer.None;
        trainlifeTimer = TickTimer.CreateFromSeconds(Runner, lifeTime);
    }
    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority) return;
        if (trainlifeTimer.Expired(Runner))
        {
            if (Object != null)
                Runner.Despawn(Object);
        }
        else
            transform.localPosition += transform.forward * speed * Runner.DeltaTime;
    }
}
