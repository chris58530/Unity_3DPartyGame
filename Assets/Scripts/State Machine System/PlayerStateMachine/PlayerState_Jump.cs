using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Jump", fileName = "PlayerState_Jump")]

public class PlayerState_Jump : PlayerState
{
    [SerializeField]
    private float jumpForce;
    public override void Enter()
    {
        base.Enter();
        // animation
        controller.SetPlayerJump(jumpForce);
    }
    public override void LogicUpdate()
    {
       if(controller.IsFalling){
        stateMachine.SwitchState(typeof(PlayerState_Fall));
       }
    }
}
