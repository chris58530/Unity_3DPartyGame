using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Idle", fileName = "PlayerState_Idle")]

public class PlayerState_Idle : NetworkPlayerState
{

    public override void Enter()
    {
        base.Enter();
    }


    public override void UpdateNetwork(NetworkInputData inputData)
    {
        base.UpdateNetwork(inputData);
        if (inputData.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Walk));
            
        }
        if (inputData.IsLeftPressed)
        {
            controller.IsBall = true;
            stateMachine.SwitchState(typeof(PlayerState_Rush));
        }
        if (inputData.IsJumpPressed)
        {
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }

        if (!controller.IsGround)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
        if (controller.IsStun)
        {
            stateMachine.SwitchState(typeof(PlayerState_FallToGround));
        }
    


    }
}
