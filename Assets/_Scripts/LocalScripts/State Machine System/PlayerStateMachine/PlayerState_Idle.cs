using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Idle", fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState
{

    public override void Enter()
    {
        base.Enter();
        controller.SwitchModel(1);
    }

    public override void LogicUpdate()
    {
        if (moveInput.Move)
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
        if(controller.IsStun){
            stateMachine.SwitchState(typeof(PlayerState_FallToGround));
        }
    }
    public override void PhysicUpdate()
    {

    }
    public override void Exit()
    {

    }
}
