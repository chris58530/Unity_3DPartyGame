using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Idle", fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState
{
    [SerializeField]
    private float deceleration;
    public override void Enter()
    {
        currentSpeed = playerController.MoveSpeed;

    }
    public override void LogicUpdate()
    {
        if (Input.GetKey(playerMoveInput.forwardArrow) || Input.GetKey(playerMoveInput.backArrow) || Input.GetKey(playerMoveInput.leftArrow) || Input.GetKey(playerMoveInput.rightArrow))
        {
            playerStateMachine.SwitchState(typeof(PlayerState_Walk));
        }
        currentSpeed = Vector3.MoveTowards(currentSpeed, new Vector3(0, 0, 0), deceleration * Time.deltaTime);

    }
    public override void PhysicUpdate()
    {
        // playerController.SetPlayerVelocity(currentSpeed);

    }
    public override void Exit()
    {
        Debug.Log("Exit Animation");

    }
}
