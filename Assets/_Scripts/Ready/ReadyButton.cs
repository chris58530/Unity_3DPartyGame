using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class ReadyButton : NetworkBehaviour
{
    public NetworkBool isDown;
    private float originalPos;
    private float minY = 0.2f;

    public override void Spawned()
    {
        originalPos = transform.position.y;
        isDown = false;
    }
    public override void FixedUpdateNetwork()
    {
        DetectCollision();
    }
    // private void OnCollisionStay(Collision other)
    // {
    //     if (other.gameObject.GetComponent<Rigidbody>())
    //     {
    //         Down();
    //         isDown = true;
    //     }
    // }
    // private void OnCollisionExit(Collision other)
    // {
    //     if (other.gameObject.GetComponent<Rigidbody>())
    //     {
    //         isDown = false;
    //     }
    // }
    public void Down()
    {
        if (transform.position.y >= minY)
            transform.position += Vector3.down * Runner.DeltaTime * 2;
    }
    public void Up()
    {
        // if (isDown) return;
        if (transform.position.y <= originalPos)
            transform.position += Vector3.up * Runner.DeltaTime;
    }
    private void DetectCollision()
    {
        if (Object == null) return;
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (var Collider in colliders)
        {
            if (Collider.GetComponent<NetworkObject>())
            {
                Down();
                Debug.Log("down");
            }
            else
            {
                Up();
                Debug.Log("up");
            }
        }
        if (colliders == null)
        {

        }
    }
}
