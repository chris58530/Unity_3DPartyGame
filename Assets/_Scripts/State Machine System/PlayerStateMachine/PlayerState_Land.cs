using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Land", fileName = "PlayerState_Land")]

public class PlayerState_Land : NetworkPlayerState
{
    // public override void Enter()
    // {
    //     base.Enter();
    // }
    // public override void LogicUpdate()
    // {
    //     if (moveInput.Jump)
    //     {
    //         stateMachine.SwitchState(typeof(PlayerState_Jump));
    //     }
    //     if (IsAnimationFinish)
    //     {
    //         if (moveInput.Move)
    //         {
    //             if (moveInput.speedtime > controller.switchToRush)
    //                 stateMachine.SwitchState(typeof(PlayerState_Rush));
    //             else
    //                 stateMachine.SwitchState(typeof(PlayerState_Walk));

    //         }
    //         if (!moveInput.Move)
    //         {
    //             stateMachine.SwitchState(typeof(PlayerState_Idle));
    //         }
    //     }
    //     if (controller.IsStun)
    //     {
    //         stateMachine.SwitchState(typeof(PlayerState_FallToGround));
    //     }
    // }
    // public override void PhysicUpdate()
    // {
    //     if (moveInput.speedtime > controller.switchToRush)
    //     {
    //         controller.SetPlayerAddForce(controller.rushSpeed);
    //     }
    //     else
    //     {
    //         controller.SetPlayerAddForce(controller.walkSpeed);
    //     }
    // }

}
