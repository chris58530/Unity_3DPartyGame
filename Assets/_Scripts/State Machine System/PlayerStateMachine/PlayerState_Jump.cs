using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Jump", fileName = "PlayerState_Jump")]

public class PlayerState_Jump : NetworkPlayerState
{
    [SerializeField]
    private float jumpForce;

    [Header("Player Jump Speed"), SerializeField]
    private float moveSpeed;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("jump");


        // controller.SetPlayerJump(jumpForce);
    }
    public override void UpdateNetwork()
    {
        if(moveInput.StopJump)
        {
              stateMachine.SwitchState(typeof(PlayerState_Idle));
        }

    //     if (controller.IsFalling || moveInput.StopJump)
    //     {
    //         stateMachine.SwitchState(typeof(PlayerState_Fall));
    //     }
    //     if (controller.IsStun)
    //     {
    //         stateMachine.SwitchState(typeof(PlayerState_FallToGround));
    //     }
    }
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