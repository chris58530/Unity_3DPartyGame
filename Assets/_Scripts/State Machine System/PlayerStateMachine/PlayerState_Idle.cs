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
        if (inputData.Move)//host可以控制自己 其他人不能動
        {
            stateMachine.SwitchState(typeof(PlayerState_Walk));
        }
        if (inputData.IsJumpPressed)
        {
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }

        if (inputData.IsShootPressed)

        {
            shooter.ShootMagnet(inputData);
        }
        if (!controller.IsGround)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
        if (controller.IsStun)
        {
            stateMachine.SwitchState(typeof(PlayerState_FallToGround));
        }
        // shooter.OpenMagnet(inputData);
        if (inputData.IsOpenPressed)
        {
            shooter.OpenTrigger += 1;
            controller.AngryValue += 10;

        }
        if (inputData.StopOpen)
        {
            shooter.CloseTrigger += 1;
        }

    }
}
