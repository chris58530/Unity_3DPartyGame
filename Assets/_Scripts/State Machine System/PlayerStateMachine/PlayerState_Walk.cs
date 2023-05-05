using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Walk", fileName = "PlayerState_Walk")]
public class PlayerState_Walk : NetworkPlayerState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("walk");

    }

    public override void UpdateNetwork(NetworkInputData inputData)
    {

        if (!inputData.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        controller.SetPlayerMove(inputData);

        if (inputData.SpeedTime > controller.switchToRush)
        {
            stateMachine.SwitchState(typeof(PlayerState_Rush));
        }
        if (inputData.IsJumpPressed)
        {
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }
        // if (!controller.IsGround)
        // {
        //     stateMachine.SwitchState(typeof(PlayerState_Fall));
        // }
        // if (controller.IsStun)
        // {
        //     stateMachine.SwitchState(typeof(PlayerState_FallToGround));
        // }

    }


}
