using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class UnLimitRolling : NetworkBehaviour
{
    [SerializeField]
    private Transform startTrans;

    [SerializeField]
    private Transform endTrans;
    [SerializeField]
    private float moveSpeed;

    public override void FixedUpdateNetwork()
    {
        transform.position += new Vector3(0,0,-1) * moveSpeed * Runner.DeltaTime;

        if (transform.position.z < endTrans.position.z)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y,startTrans.position.z);
        }
    }
}

