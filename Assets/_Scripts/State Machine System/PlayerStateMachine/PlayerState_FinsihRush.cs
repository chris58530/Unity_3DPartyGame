using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/FinsihRush", fileName = "PlayerState_FinsihRush")]

public class PlayerState_FinsihRush : NetworkPlayerState
{
    public override void Enter()
    {
        base.Enter();
    }

    // public override void LogicUpdate()
    // {
    //     if (IsAnimationFinish)
    //         stateMachine.SwitchState(typeof(PlayerState_Walk));

    //     if (moveInput.Jump)
    //         stateMachine.SwitchState(typeof(PlayerState_Jump));

    //     if (!controller.IsGround)
    //         stateMachine.SwitchState(typeof(PlayerState_Fall));
    //     if (controller.IsStun)
    //     {
    //         stateMachine.SwitchState(typeof(PlayerState_FallToGround));
    //     }
    // }
    // public override void PhysicUpdate()
    // {
    //     controller.SetPlayerAddForce(controller.rushSpeed);
    // }
    // public override void Exit()
    // {
    //     base.Exit();
    //     controller.SwitchTag("Walk");
    //     moveInput.ShowRushSpeed(false);//關閉 speed 文字

    // }

}
