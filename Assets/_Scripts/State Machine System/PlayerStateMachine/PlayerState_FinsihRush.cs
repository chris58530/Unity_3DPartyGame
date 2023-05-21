using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/FinsihRush", fileName = "PlayerState_FinsihRush")]

public class PlayerState_FinsihRush : NetworkPlayerState
{
    public override void Enter()
    {
        base.Enter();
    }
    public override void UpdateNetwork(NetworkInputData inputData)
    {
        base.UpdateNetwork(inputData);
        if (IsAnimationFinish)
            stateMachine.SwitchState(typeof(PlayerState_Walk));

        if (inputData.IsJumpPressed)
            stateMachine.SwitchState(typeof(PlayerState_Jump));

        if (!controller.IsGround)
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        if (controller.IsStun)
        {
            stateMachine.SwitchState(typeof(PlayerState_FallToGround));
        }
        controller.SetPlayerRush(inputData);
        if (inputData.IsOpenPressed)
        {
            shooter.PowerTrigger += 1;

        }
        if (inputData.StopOpen)
        {
            shooter.ClosePowerTrigger += 1;
        }
    }

    public override void Exit()
    {
        base.Exit();
        // controller.SwitchTag("Walk");
        // moveInput.ShowRushSpeed(false);//關閉 speed 文字

    }

}
