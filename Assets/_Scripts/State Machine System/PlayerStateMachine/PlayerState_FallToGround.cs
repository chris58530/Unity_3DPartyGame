using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/FallToGround", fileName = "PlayerState_FallToGround")]

public class PlayerState_FallToGround : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        controller.IsStun = false; //重製暈眩值
    }

    public override void LogicUpdate()
    {
        if (IsAnimationFinish)
            stateMachine.SwitchState(typeof(PlayerState_Stun));
    }
    public override void PhysicUpdate()
    {

    }
    public override void Exit()
    {

    }
}
