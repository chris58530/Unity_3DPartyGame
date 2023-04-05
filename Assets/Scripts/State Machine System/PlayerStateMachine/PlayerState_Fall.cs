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
    public override void LogicUpdate()
    {
        if(controller.IsGround){
            stateMachine.SwitchState(typeof(PlayerState_Land));
        }
        Debug.Log("FALL");

    }
    public override void PhysicUpdate()
    {
       
        controller.SetPlayerAddForceY( speedCurve.Evaluate(StateDuration));
        float v = moveInput.moveInput.x;
        float h = moveInput.moveInput.y;
        Vector3 lookAt = new Vector3(h, 0, v);

        controller.SetPlayerAddForce(lookAt, moveSpeed);
    }
}
