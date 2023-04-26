using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObject : HitObject, IStrikeable
{
    private void Start()
    {
        rb.mass = 3;
    }
    public void Knock(Vector3 forcePoint, float force)
    {
        Vector3 direction = new Vector3(forcePoint.x, 0, forcePoint.z);
        rb.AddForce(-direction * knockForce * force, ForceMode.Impulse);
        this.tag = "KnockingObject";
    }
}