using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Idle", fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState
{

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        if (Input.GetKey(moveInput.forwardArrow) || Input.GetKey(moveInput.backArrow) || Input.GetKey(moveInput.leftArrow) || Input.GetKey(moveInput.rightArrow))
        {
            stateMachine.SwitchState(typeof(PlayerState_Walk));
        }
        if (moveInput.Jump)
        {
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }
        if (!controller.IsGround)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
        Debug.Log("idle");
    }
    public override void PhysicUpdate()
    {

    }
    public override void Exit()
    {
        Debug.Log("Exit Animation");

    }
}
