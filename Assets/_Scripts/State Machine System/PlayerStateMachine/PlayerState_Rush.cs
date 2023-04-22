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
        controller.SwitchTag("Rush");
         controller.SwitchModel(2);

    }

    public override void LogicUpdate()
    {
      


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
    

        controller.SetPlayerAddForce(controller.rushSpeed);

    }
    public override void Exit()
    {
        base.Exit();
        controller.SwitchTag("Walk");

    }

}
