using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObject : HitObject, IStrikeable
{
     private void Start() {
        rb.mass = 3;    
    }
    public void Knock(Vector3 forcePoint,float force)
    {
        rb.AddForce(-forcePoint*knockForce*force,ForceMode.Impulse);
        this.tag = "KnockingObject";
    }
}
