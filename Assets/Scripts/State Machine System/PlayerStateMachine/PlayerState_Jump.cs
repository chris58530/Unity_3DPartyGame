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
        if (controller.IsFalling)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
    }
    public override void PhysicUpdate()
    {

        float v = moveInput.moveInput.x;
        float h = moveInput.moveInput.y;
        Vector3 lookAt = new Vector3(h, 0, v);

        if (moveInput.speedtime > controller.switchToRush)

        {
            controller.SetPlayerAddForce(lookAt, controller.rushSpeed);

        }
        else
        {

            controller.SetPlayerAddForce(lookAt, controller.walkSpeed);

        }
    }
}
