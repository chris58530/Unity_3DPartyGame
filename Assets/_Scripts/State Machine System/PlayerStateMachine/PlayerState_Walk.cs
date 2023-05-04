using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Walk", fileName = "PlayerState_Walk")]
public class PlayerState_Walk : NetworkPlayerState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("walk");

    }
    
    public override void UpdateNetwork()
    {
        if (!moveInput.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        controller.SetPlayerAddForce(100);
        
        // if (moveInput.speedtime > controller.switchToRush)
        // {
        //     stateMachine.SwitchState(typeof(PlayerState_Rush));
        // }
        // if (moveInput.Jump)
        // {
        //     stateMachine.SwitchState(typeof(PlayerState_Jump));
        // }
        // if (!controller.IsGround)
        // {
        //     stateMachine.SwitchState(typeof(PlayerState_Fall));
        // }
        // if (controller.IsStun)
        // {
        //     stateMachine.SwitchState(typeof(PlayerState_FallToGround));
        // }

    }
    // public override void PhysicUpdate()
    // {
    //     float v = moveInput.AxisX;
    //     float h = moveInput.AxisZ;
    //     Vector3 lookAt = new Vector3(h, 0, v);

    //     controller.SetPlayerAddForce(controller.walkSpeed);

    // }
 
}
