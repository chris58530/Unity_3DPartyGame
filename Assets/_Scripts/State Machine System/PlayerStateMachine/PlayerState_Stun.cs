using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Stun", fileName = "PlayerState_Stun")]

public class PlayerState_Stun : PlayerState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        if (!controller.IsStun)
            stateMachine.SwitchState(typeof(PlayerState_WakeUp));
        if (!controller.IsGround)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }

    }
    public override void PhysicUpdate()
    {

    }
    public override void Exit()
    {

    }
}
