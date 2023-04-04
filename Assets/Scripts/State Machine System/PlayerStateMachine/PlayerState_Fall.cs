using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Fall", fileName = "PlayerState_Fall")]

public class PlayerState_Fall : PlayerState
{
    [SerializeField]
    private AnimationCurve speedCurve;
    public override void LogicUpdate()
    {
        if(controller.IsGround){
            stateMachine.SwitchState(typeof(PlayerState_Land));
        }
        Debug.Log("FALL");

    }
    public override void PhysicUpdate()
    {
        speedCurve.Evaluate(StateDuration);
      
    }
}
