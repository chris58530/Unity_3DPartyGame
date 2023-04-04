using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Walk", fileName = "PlayerState_Walk")]
public class PlayerState_Walk : PlayerState
{
    [Header("Player Move Speed"), SerializeField]
    private float moveSpeed;

    [Header("Player Jump Force"), SerializeField]
    private float jumpForce;
    [Header("Switch Rush Value"), SerializeField]

    private float switchToRush;
    public override void Enter()
    {
    }
    public override void LogicUpdate()
    {
        if (!(Input.GetKey(moveInput.forwardArrow) || Input.GetKey(moveInput.backArrow) || Input.GetKey(moveInput.leftArrow) || Input.GetKey(moveInput.rightArrow)))
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        if (moveInput.speedtime > switchToRush)
        {
            stateMachine.SwitchState(typeof(PlayerState_Rush));
        }
        if (moveInput.Jump)
        {
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }
        if (!controller.IsGround)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }

    }
    public override void PhysicUpdate()
    {
        float v = moveInput.moveInput.x;
        float h = moveInput.moveInput.y;
        Vector3 lookAt = new Vector3(h, 0, v);

        controller.SetPlayerAddForce(lookAt, moveSpeed);

    }
    public override void Exit()
    {

    }
}
