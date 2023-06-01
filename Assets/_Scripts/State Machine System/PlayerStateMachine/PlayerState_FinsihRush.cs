using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/FinsihRush", fileName = "PlayerState_FinsihRush")]

public class PlayerState_FinsihRush : NetworkPlayerState
{
    public override void Enter()
    {
        base.Enter();
        particle.RPC_StopParticle(EffectType.RushLV2);
        particle.RPC_StopParticle(EffectType.RushLV3);
    }
    public override void UpdateNetwork(NetworkInputData inputData)
    {
        particle.RPC_StopParticle(EffectType.RushLV2);
        particle.RPC_StopParticle(EffectType.RushLV3);
        base.UpdateNetwork(inputData);

        if (StateDuration > animator.ani.GetCurrentAnimatorStateInfo(0).length)
            stateMachine.SwitchState(typeof(PlayerState_Idle));

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
            if (shooter == null) return;
            float value = controller.AngryValue;

            shooter.PowerTrigger = value;

        }

    }

    public override void Exit()
    {
        base.Exit();
        // controller.SwitchTag("Walk");
        // moveInput.ShowRushSpeed(false);//關閉 speed 文字

    }

}
