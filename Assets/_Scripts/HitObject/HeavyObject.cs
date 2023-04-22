using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyObject : HitObject, IStrikeable
{
    private void Start()
    {
        rb.mass = 5;
    }
    public void Knock(Vector3 forcePoint, float force)
    {
        rb.AddForce(-forcePoint * knockForce * force, ForceMode.Impulse);
        this.tag = "KnockingObject";
    }
}
