using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerGroundDetector : NetworkBehaviour
{
    [SerializeField,Range(0.1f,1f)]
    private float detectionRadius = 0.1f;
    [SerializeField]
    private LayerMask layer;
    Collider[] colliders = new Collider[1];
    public NetworkBool IsGround;
    
    public override void FixedUpdateNetwork()
    {
        if (Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, colliders, layer) != 0)
        {
            IsGround = true;
        }
        else IsGround = false;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
