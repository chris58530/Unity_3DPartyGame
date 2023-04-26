using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{
    [SerializeField,Range(0.1f,1f)]
    private float detectionRadius = 0.1f;
    [SerializeField]
    private LayerMask layer;
    Collider[] colliders = new Collider[1];
    public bool IsGround
    {
        get
        {
            return Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, colliders, layer) != 0;

        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
