using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkRB : NetworkBehaviour
{
    Rigidbody rb;
    //5j4ur
    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData inputData))
        {
            Vector3 moveDir = new Vector3(inputData.AxisX,0,inputData.AxisZ);
            if (inputData.Move)
            {
                var rotation = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 8);
            }
            rb.AddForce(moveDir * 100);
        }
    }
}
