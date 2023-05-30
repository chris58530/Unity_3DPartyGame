using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class HeavyObject : NetworkBehaviour
{

    [Networked]
    public float KnockForce { get; set; }
    public override void Spawned()
    {
        this.tag = "Untouched";
    }

}
