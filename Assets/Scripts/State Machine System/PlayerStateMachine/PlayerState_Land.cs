using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Land", fileName = "PlayerState_Land")]

public class PlayerState_Land : PlayerState
{
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        if (moveInput.Jump)
        {
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }
        if (Input.GetKey(moveInput.forwardArrow) || Input.GetKey(moveInput.backArrow) || Input.GetKey(moveInput.leftArrow) || Input.GetKey(moveInput.rightArrow))
        {
            if (moveInput.speedtime > controller.switchToRush)
                stateMachine.SwitchState(typeof(PlayerState_Walk));
        }
        if (!Input.GetKey(moveInput.forwardArrow) || Input.GetKey(moveInput.backArrow) || Input.GetKey(moveInput.leftArrow) || Input.GetKey(moveInput.rightArrow))
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }

    }

}
