using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Jump", fileName = "PlayerState_Jump")]

public class PlayerState_Jump : NetworkPlayerState
{


    [Header("Player Jump Speed"), SerializeField]
    private float moveSpeed;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("jump");


        controller.SetPlayerJump();
    }
    public override void UpdateNetwork(NetworkInputData inputData)
    {
        if(!inputData.IsJumpPressed)
        {
              stateMachine.SwitchState(typeof(PlayerState_Idle));
        }

        if (controller.IsFalling || !inputData.IsJumpPressed)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
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
