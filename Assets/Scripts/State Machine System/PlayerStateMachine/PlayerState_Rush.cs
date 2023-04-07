using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Rush", fileName = "PlayerState_Rush")]

public class PlayerState_Rush : PlayerState
{
    [SerializeField]
    private float jumpForce;



    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        if(StateDuration>0.12f){
        controller.SwitchModel(2);

        }
        
        
        if (moveInput.speedtime <= controller.switchToRush)
        {
            controller.SwitchModel(1);

            stateMachine.SwitchState(typeof(PlayerState_Walk));
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

        controller.SetPlayerAddForce(lookAt, controller.rushSpeed);

    }
    public override void Exit()
    {
        base.Exit();
    }
}
