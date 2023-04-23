using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Rush", fileName = "PlayerState_Rush")]

public class PlayerState_Rush : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        controller.SwitchTag("Rush");
        controller.SwitchModel(2);
    }

    public override void LogicUpdate()
    {
        if (moveInput.speedtime <= controller.switchToRush)
            stateMachine.SwitchState(typeof(PlayerState_FinsihRush));
        if (moveInput.Jump)
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        if (!controller.IsGround)
            stateMachine.SwitchState(typeof(PlayerState_Fall));

        moveInput.ShowRushSpeed(true);//開啟 speed 文字
        if (controller.IsStun)
        {
            stateMachine.SwitchState(typeof(PlayerState_FallToGround));
        }
    }
    public override void PhysicUpdate()
    {
        float addItionSpeed = moveInput.speedtime;
        controller.SetPlayerAddForce(controller.rushSpeed + addItionSpeed);
    }
    public override void Exit()
    {
        controller.SwitchModel(1);
        controller.SwitchTag("Walk");

    }

}
