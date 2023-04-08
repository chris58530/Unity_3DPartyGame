using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Walk", fileName = "PlayerState_Walk")]
public class PlayerState_Walk : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        controller.SwitchModel(1);

    }
    public override void LogicUpdate()
    {
        if (!moveInput.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        if (moveInput.speedtime > controller.switchToRush)
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
        // float v = moveInput.AxisX;
        // float h = moveInput.AxisZ;
        // Vector3 lookAt = new Vector3(h, 0, v);

        controller.SetPlayerAddForce(controller.walkSpeed);

    }
    public override void Exit()
    {

    }
}
