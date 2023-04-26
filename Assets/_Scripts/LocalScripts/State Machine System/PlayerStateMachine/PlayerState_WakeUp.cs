using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/WakeUp", fileName = "PlayerState_WakeUp")]

public class PlayerState_WakeUp : PlayerState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        if (IsAnimationFinish)
            stateMachine.SwitchState(typeof(PlayerState_Idle));
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
