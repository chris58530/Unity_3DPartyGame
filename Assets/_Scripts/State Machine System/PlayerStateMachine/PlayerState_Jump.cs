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
            stateMachine.SwitchState(typeof(PlayerState_Fall));
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
            if (shooter == null) return;
            float value = controller.AngryValue;

            shooter.PowerTrigger = value;

        }
         if (inputData.IsOpenPressed)
        {
                       controller.IsBall = true;

            stateMachine.SwitchState(typeof(PlayerState_Rush));
        }
    }

}
