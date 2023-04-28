using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Fall", fileName = "PlayerState_Fall")]

public class PlayerState_Fall : NetworkPlayerState
{

    // [SerializeField]
    // private AnimationCurve speedCurve;
    // [Header("Player Fall Speed"), SerializeField]
    // private float moveSpeed;

    // public override void Enter()
    // {
    //     base.Enter();
    // }
    // public override void LogicUpdate()
    // {
    //     if (controller.IsGround)
    //     {
    //         if (moveInput.speedtime > controller.switchToRush)
    //             stateMachine.SwitchState(typeof(PlayerState_Rush));
    //         else
    //             stateMachine.SwitchState(typeof(PlayerState_Land));
    //     }
     


    // }
    // public override void PhysicUpdate()
    // {

    //     controller.SetPlayerFallDown(speedCurve.Evaluate(StateDuration));
    //     if (controller.IsStun)
    //         return;

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
