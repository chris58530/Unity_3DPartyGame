using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Rush", fileName = "PlayerState_Rush")]

public class PlayerState_Rush : NetworkPlayerState
{
    public override void Enter()
    {
        base.Enter();

        controller.SwitchTag("Rush");
        particle.RPC_PlayParticle(EffectType.RushLV1);

        controller.modelCount = 1;
    }
    public override void UpdateNetwork(NetworkInputData inputData)
    {
        base.UpdateNetwork(inputData);
        controller.SetPlayerRush(inputData);


        if (inputData.IsShootPressed)
        {
            controller.IsBall = false;
            stateMachine.SwitchState(typeof(PlayerState_FinsihRush));
        }
        if (inputData.IsJumpPressed)
        {
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }

        if (!controller.IsGround)
        {
            stateMachine.SwitchState(typeof(PlayerState_FinsihRush));
        }
        if (controller.IsStun)
        {
            stateMachine.SwitchState(typeof(PlayerState_FallToGround));
        }
        if (inputData.IsOpenPressed)
        {
            if (shooter == null) return;
            float value = controller.AngryValue;
            shooter.PowerTrigger = value;
        }
        if (controller.SpeedTime > 2)
        {
            particle.RPC_PlayParticle(EffectType.RushLV2);

        }

        if (controller.SpeedTime > 5)
        {
            particle.RPC_PlayParticle(EffectType.RushLV3);

        }

    }

    public override void Exit()
    {
        particle.RPC_StopParticle(EffectType.RushLV1);
        // if (controller.SpeedTime <= 5)
        // {
        //     particle.RPC_StopParticle(EffectType.RushLV2);
        // }
        // else particle.RPC_StopParticle(EffectType.RushLV2);
        particle.RPC_StopParticle(EffectType.RushLV2);
        particle.RPC_StopParticle(EffectType.RushLV3);
        particle.RPC_StopAllParticle();
        controller.SwitchTag("Walk");
        controller.modelCount = 0;
    }

}
