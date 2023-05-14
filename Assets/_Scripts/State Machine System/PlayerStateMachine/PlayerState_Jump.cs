using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Jump", fileName = "PlayerState_Jump")]

public class PlayerState_Jump : NetworkPlayerState
{
    public override void Enter()
    {
        base.Enter();
        controller.SetPlayerJump();
    }
    public override void UpdateNetwork(NetworkInputData inputData)
    {
        base.UpdateNetwork(inputData);
        if (!inputData.IsJumpPressed)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }

        if (controller.IsFalling || !inputData.IsJumpPressed)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
        if (controller.IsStun)
        {
            stateMachine.SwitchState(typeof(PlayerState_FallToGround));
        }

        if (controller.SpeedTime > controller.switchToRush)
        {
            controller.SetPlayerRush(inputData);
        }
        else
        {
            controller.SetPlayerMove(inputData);
        }
        if (inputData.IsOpenPressed)
        {
            shooter.OpenTrigger += 1;

        }
        if (inputData.StopOpen)
        {
            shooter.CloseTrigger += 1;
        }
    }

}
