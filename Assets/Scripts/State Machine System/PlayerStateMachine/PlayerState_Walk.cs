using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Walk", fileName ="PlayerState_Walk")]
public class PlayerState_Walk : PlayerState
{
    [Header("Player Move Speed"),SerializeField]
    private float moveSpeed;
    [Header("Player Speed Addition(Walk)"),SerializeField]
    private float speedAddition;
    [Header("Player Jump Force"),SerializeField]
    private float jumpForce;
    [Header("Switch Rush Value"),SerializeField]
    private float switchtoRush;

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
        if (playerMoveInput.speedtime > switchtoRush)
        {
            playerStateMachine.SwitchState(typeof(PlayerState_Rush));
        }
        
    }
    public override void PhysicUpdate()
    {
        float v = playerMoveInput.moveInput.x;
        float h = playerMoveInput.moveInput.y;
        Vector3 lookAt = new Vector3(h, 0, v);
        float speedtime = playerMoveInput.speedtime * speedAddition;

       // playerController.SetPlayerVelocity(lookAt, (moveSpeed + speedtime));
        playerController.SetPlayerAddForece(lookAt, (moveSpeed + speedtime));

    }
    public override void Exit()
    {
        playerController.SetPlayerJump(jumpForce*10000);
        Debug.Log("jump");

    }
}
