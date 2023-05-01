using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerState_Idle : NetworkPlayerState
{

    public override void Enter()
    {
        base.Enter();
        Debug.Log("idle");
    }

    public override void UpdateNetwork()
    {
    
        if (moveInput.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Walk));
        }
        if (moveInput.Jump)
        {
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }

    }

    public override void Exit()
    {

    }
}
