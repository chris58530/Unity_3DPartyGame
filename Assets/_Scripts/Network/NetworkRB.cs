using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkRB : NetworkBehaviour
{
    [Networked] private TickTimer life { get; set; }


    private void Start()
    {
    }

    public void Init(Vector3 forward)
    {
        life = TickTimer.CreateFromSeconds(Runner, 5.0f);
        GetComponent<Rigidbody>().velocity = forward;
    }

    public override void FixedUpdateNetwork()
    {
            GetComponent<Rigidbody>().AddForce(new Vector3(10, 0, 0));

        if (life.Expired(Runner))
            Runner.Despawn(Object);
    }
    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var inputData = new NetworkInputData();

        inputData.buttons.Set(1, Input.GetKey(KeyCode.A));

        input.Set(inputData);
    }
}
