using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Rush", fileName = "PlayerState_Rush")]

public class PlayerState_Rush : PlayerState
{
    [Header("Player Move Speed"),SerializeField]
    private float moveSpeed;
    [Header("Player Speed Addition(Rush)"),SerializeField]
    private float speedAddition;

    public override void Enter()
    {
        Debug.Log("Rush Animation");
    }
    public override void LogicUpdate()
    {
        if (!(Input.GetKey(playerMoveInput.forwardArrow) || Input.GetKey(playerMoveInput.backArrow) || Input.GetKey(playerMoveInput.leftArrow) || Input.GetKey(playerMoveInput.rightArrow)))
        {
            playerStateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        if (playerMoveInput.speedtime <= 1)
        {
            playerStateMachine.SwitchState(typeof(PlayerState_Walk));
        }

    }
    public override void PhysicUpdate()
    {
        float v = playerMoveInput.moveInput.x;
        float h = playerMoveInput.moveInput.y;
        Vector3 lookAt = new Vector3(h, 0, v);
        float speedtime = playerMoveInput.speedtime * speedAddition;

        playerController.SetPlayerAddForece(lookAt, (moveSpeed + speedtime));

    }
    public override void Exit()
    {
        base.Exit();
    }
}
