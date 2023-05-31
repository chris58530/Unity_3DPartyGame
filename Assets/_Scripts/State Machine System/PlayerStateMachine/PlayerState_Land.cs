using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Land", fileName = "PlayerState_Land")]

public class PlayerState_Land : NetworkPlayerState
{
    public override void Enter()
    {
        base.Enter();
        particle.RPC_PlayParticle(EffectType.Land);

    }

    public override void UpdateNetwork(NetworkInputData inputData)
    {
        base.UpdateNetwork(inputData);

        if (controller.IsBall)
            stateMachine.SwitchState(typeof(PlayerState_Rush));
        if (StateDuration > animator.ani.GetCurrentAnimatorStateInfo(0).length)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
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
    public override void Exit()
    {
        // particle.RPC_StopParticle(EffectType.Land);
    }

}
