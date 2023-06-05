using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class TruckManager : NetworkBehaviour
{

    [SerializeField]
    private Truck[] truck;


    [SerializeField]
    private float truckLeaveRate = 5;

    [Networked]
    private TickTimer truckLeaveTimer { get; set; }



    public override void Spawned()
    {
        truckLeaveTimer = TickTimer.CreateFromSeconds(Runner, truckLeaveRate);
        AudioManager.Instance.RPC_PlaySFX("Truck");


    }
    public override void FixedUpdateNetwork()
    {
        if (truckLeaveTimer.Expired(Runner))
        {
            truckLeaveTimer = TickTimer.CreateFromSeconds(Runner, truckLeaveRate);

            int random = Random.Range(0, 2);
            Debug.Log($"{random}");
            for (int i = 0; i < truck.Length; i++)
            {
                if (i == random)
                {
                    truck[i].canMove = true;
                    AudioManager.Instance.RPC_PlaySFX("Truck");

                }

            }
        }
    }
}
