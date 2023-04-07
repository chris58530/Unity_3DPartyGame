using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Fall", fileName = "PlayerState_Fall")]

public class PlayerState_Fall : PlayerState
{

    [SerializeField]
    private AnimationCurve speedCurve;
    [Header("Player Fall Speed"), SerializeField]
    private float moveSpeed;

    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        if (controller.IsGround)
        {
            stateMachine.SwitchState(typeof(PlayerState_Land));
        }
        Debug.Log("FALL");

    }
    public override void PhysicUpdate()
    {

        controller.SetPlayerFallDown(speedCurve.Evaluate(StateDuration));
        float v = moveInput.moveInput.x;
        float h = moveInput.moveInput.y;
        Vector3 lookAt = new Vector3(h, 0, v);
        if (moveInput.speedtime > controller.switchToRush)

        {
            controller.SetPlayerAddForce(lookAt, controller.rushSpeed);

        }
        else
        {

            controller.SetPlayerAddForce(lookAt, controller.walkSpeed);

        }

    }
}
