using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Stun", fileName = "PlayerState_Stun")]

public class PlayerState_Stun : NetworkPlayerState
{
    public override void Enter()
    {
        base.Enter();
        controller.AngryValue += 50;
        Actions.PlayEffect?.Invoke(EffectType.Stun);

        particle.RPC_PlayParticle(EffectType.Stun);

    }

    public override void UpdateNetwork(NetworkInputData inputData)
    {
        base.UpdateNetwork(inputData);
        if (!controller.IsStun)
            stateMachine.SwitchState(typeof(PlayerState_WakeUp));
        if (!controller.IsGround)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
        if (inputData.IsOpenPressed)
        {
            if (shooter == null) return;
            float value = controller.AngryValue;
            if (value <= (float)PowerValue.Power2)return;

                shooter.PowerTrigger = value;

        }

    }
    public override void Exit()
    {
        Actions.StopEffect?.Invoke(EffectType.Stun);
        particle.RPC_StopParticle(EffectType.Stun);

    }
}
