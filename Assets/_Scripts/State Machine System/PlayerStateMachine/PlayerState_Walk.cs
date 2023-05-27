using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Walk", fileName = "PlayerState_Walk")]
public class PlayerState_Walk : NetworkPlayerState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateNetwork(NetworkInputData inputData)
    {
        base.UpdateNetwork(inputData);
        controller.SetPlayerMove(inputData);
        if (!inputData.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }

        if (controller.SpeedTime > controller.switchToRush)
        {
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
        if (inputData.IsOpenPressed)
        {
            float value = controller.AngryValue;

            shooter.PowerTrigger = value;

        }

    }
    public override void Exit()
    {
    }


}
