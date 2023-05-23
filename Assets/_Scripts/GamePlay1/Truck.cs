using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Truck : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnMove))]
    public NetworkBool canMove { get; set; }
    // [Networked(OnChanged = nameof(OnBack))]
    // public NetworkBool canBack { get; set; }
    private float speed = 3f;
    private float addition = 0;
    private float timeToGoBack = 10;
    [Networked]
    private TickTimer timeToBackTimer { get; set; }
    [SerializeField]
    private Transform originalTransZ;
    [SerializeField]
    private Transform originalNextTransZ;

    [Networked]
    private int truckRandomPos { get; set; }

    [Networked]
    private TickTimer randomTimer { get; set; }
    public override void Spawned()
    {
        randomTimer = TickTimer.CreateFromSeconds(Runner, 2);
    }

    public override void FixedUpdateNetwork()
    {
        // canBack = false;
        if (randomTimer.Expired(Runner))
        {
            truckRandomPos = Random.Range(0, 20);
            randomTimer = TickTimer.CreateFromSeconds(Runner, 1);

        }

        if (!canMove)
        {
            RandomTruckTransformZ();
        addition = 0;
        }
        if (!canMove) return;
        addition += 0.01f;
        transform.position += Vector3.forward * (speed + addition) * Runner.DeltaTime;
        Debug.Log("truck moving...");

        if (timeToBackTimer.Expired(Runner))
        {
            Debug.Log("truck go back");
            canMove = false;
            // canBack = true;
            timeToBackTimer = TickTimer.None;
        }
    }
    private static void OnMove(Changed<Truck> changed)
    {
        if (changed.Behaviour.canMove)
            changed.Behaviour.timeToBackTimer = TickTimer.CreateFromSeconds(changed.Behaviour.Runner, changed.Behaviour.timeToGoBack);
        else
        {
            changed.Behaviour.transform.position = new Vector3(changed.Behaviour.transform.position.x, changed.Behaviour.transform.position.y, changed.Behaviour.originalTransZ.position.z);
        }
    }
    // private static void OnBack(Changed<Truck> changed)
    // {
    //     if (changed.Behaviour.canMove)
    //         changed.Behaviour.transform.position = new Vector3(0, 0, changed.Behaviour.originalTransZ.position.z);
    // }
    private void RandomTruckTransformZ()
    {
        if (transform.position.z < originalNextTransZ.position.z)
        {
            transform.position += Vector3.forward * speed * Runner.DeltaTime;
            return;
        }

        if (transform.position.z != originalNextTransZ.position.z + truckRandomPos)
        {
            float targetZ = originalNextTransZ.position.z + truckRandomPos;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, targetZ), 0.3f * Runner.DeltaTime);
        }
    }

}
