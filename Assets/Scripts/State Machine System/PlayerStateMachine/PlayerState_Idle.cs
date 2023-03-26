using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Idle",fileName ="PlayerState_Idle")]
public class PlayerState_Idle : PlayerState
{
    public override void Enter()
    {
        Debug.Log("Idle Animation");
    }
    public override void LogicUpdate()
    {
        if (Input.GetKey(playerMoveInput.forwardArrow) || Input.GetKey(playerMoveInput.backArrow)|| Input.GetKey(playerMoveInput.leftArrow)|| Input.GetKey(playerMoveInput.rightArrow))
        {
            playerStateMachine.SwitchState(typeof(PlayerState_Walk));
        }
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
