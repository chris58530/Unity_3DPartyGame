using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Walk", fileName ="PlayerState_Walk")]
public class PlayerState_Walk : PlayerState
{
    [SerializeField]
    private float moveSpeed;
    public override void Enter()
    {
        Debug.Log("Walk Animation");
    }
    public override void LogicUpdate()
    {
        if (!(Input.GetKey(playerMoveInput.forwardArrow) || Input.GetKey(playerMoveInput.backArrow) || Input.GetKey(playerMoveInput.leftArrow) || Input.GetKey(playerMoveInput.rightArrow)))
        {
            playerStateMachine.SwitchState(typeof(PlayerState_Idle));
        }
    }
    public override void PhysicUpdate()
    {
        float v = playerMoveInput.moveInput.x;
        float h = playerMoveInput.moveInput.y;

        playerController.SetPlayerVelocity(v* moveSpeed);
        playerController.SetPlayerHorizontal(h* moveSpeed);
    }
    public override void Exit()
    {
        base.Exit();
    }
}
