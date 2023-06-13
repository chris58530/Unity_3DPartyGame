using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class ReadyButton : NetworkBehaviour
{
    public NetworkBool isReady;
    public NetworkBool isDown;
    private float originalPos;
    private float minY = 0.2f;

    public override void Spawned()
    {
        originalPos = transform.position.y;
        isDown = false;
        isReady = false;
    }
    public override void FixedUpdateNetwork()
    {
        DetectCollision();
    }

    public void Down()
    {
        if (transform.position.y >= originalPos - minY)
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.8f);
        bool isColliderFound = false;

        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Rigidbody>())
            {
                Down();
                Debug.Log(collider.name);
                isColliderFound = true;
                isReady = true;
                BattleManager.Instance.CheckAllReadyButton();
                break;
            }
        }
        if (!isColliderFound)
        {
            Up();
            isReady = false;
        }

    }
}
