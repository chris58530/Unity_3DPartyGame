using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Rush", fileName = "PlayerState_Rush")]

public class PlayerState_Rush : PlayerState
{
    [Header("Player Move Speed"),SerializeField]
    private float moveSpeed;
  
    public override void Enter()
    {
    }
    public override void LogicUpdate()
    {
        if (!(Input.GetKey(moveInput.forwardArrow) || Input.GetKey(moveInput.backArrow) || Input.GetKey(moveInput.leftArrow) || Input.GetKey(moveInput.rightArrow)))
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        if (moveInput.speedtime <= 1)
        {
            stateMachine.SwitchState(typeof(PlayerState_Walk));
        }  if (moveInput.Jump)
        {
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }

    }
    public override void PhysicUpdate()
    {
        float v = moveInput.moveInput.x;
        float h = moveInput.moveInput.y;
        Vector3 lookAt = new Vector3(h, 0, v);

        controller.SetPlayerAddForce(lookAt,moveSpeed);

    }
    public override void Exit()
    {
        base.Exit();
    }
}
