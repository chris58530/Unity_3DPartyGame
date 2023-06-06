using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class Stick : NetworkBehaviour
{
    [SerializeField]
    private float rotateSpeed;

    [Networked]
    private TickTimer startTimer { get; set; }

    public override void Spawned()
    {
        startTimer = TickTimer.CreateFromSeconds(Runner, 5);
    }
    public override void FixedUpdateNetwork()
    {
        if (startTimer.Expired(Runner))
        {
            transform.localEulerAngles += new Vector3(0, 1 * rotateSpeed * Runner.DeltaTime, 0);
        }

    }
}
