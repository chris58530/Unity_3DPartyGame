using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Idle", fileName = "PlayerState_Idle")]

public class PlayerState_Idle : NetworkPlayerState
{

    public override void Enter()
    {
        base.Enter();
        Debug.Log("idle");
    }

    public override void UpdateNetwork(NetworkInputData inputData)
    {
    
        if (moveInput.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Walk));
        }
        // if (inputData.Jump)
        // {
        //     stateMachine.SwitchState(typeof(PlayerState_Jump));
        // }

    }

    public override void Exit()
    {

    }
}
