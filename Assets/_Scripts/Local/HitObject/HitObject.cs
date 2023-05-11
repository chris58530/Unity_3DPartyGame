using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class HitObject : NetworkBehaviour
{
    protected Rigidbody rb;
    [SerializeField]
    protected float knockForce;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //當速度大於 " " 時切換成擊飛物體
        if (rb.velocity.magnitude > 3)
            this.tag = "KnockingObject";
        else
            this.tag = "HitObject";
    }

}
