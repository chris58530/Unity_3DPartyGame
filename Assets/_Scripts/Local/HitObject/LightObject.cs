using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NetworkRigidbody))]

public class LightObject : NetworkBehaviour, IStrikeable
{
    private NetworkRigidbody rb;
    [SerializeField]
    public float knockForce;
    public override void Spawned()
    {
        rb = GetComponent<NetworkRigidbody>();
        rb.Rigidbody.mass = 3;

    }
    public override void FixedUpdateNetwork()
    {
        //當速度大於 " " 時切換成擊飛物體
        //改成speedtime > x 才能急非
        if (rb.Rigidbody.velocity.magnitude > 5)
            this.tag = "KnockingObject";
        else
            this.tag = "HitObject";
    }
  
    public void Knock(Vector3 forcePoint, float force)
    {
        Vector3 direction = new Vector3(forcePoint.x, 0, forcePoint.z);
        rb.Rigidbody.AddForce(-direction * knockForce * force, ForceMode.Impulse);
    }
}