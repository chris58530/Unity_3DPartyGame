using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Jump", fileName = "PlayerState_Jump")]

public class PlayerState_Jump : PlayerState
{
    [SerializeField]
    private float jumpForce;

    [Header("Player Jump Speed"), SerializeField]
    private float moveSpeed;

    public override void Enter()
    {
        base.Enter();
        // animation
        controller.SetPlayerJump(jumpForce);
    }
    public override void LogicUpdate()
    {
        if (controller.IsFalling||moveInput.StopJump)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
    
    }
    public override void PhysicUpdate()
    {

     

        if (moveInput.speedtime > controller.switchToRush)
        {
            controller.SetPlayerAddForce(controller.rushSpeed);
        }
        else
        {
            controller.SetPlayerAddForce(controller.walkSpeed);
        }
    }
}
