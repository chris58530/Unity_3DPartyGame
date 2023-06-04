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
        particle.RPC_StopParticle(EffectType.RushLV2);
        particle.RPC_StopParticle(EffectType.RushLV3);
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

        if (controller.IsBall)
        {
            controller.SetPlayerRush(inputData);
        }
        else
        {
            controller.SetPlayerMove(inputData);
        }
    
    
    }
    public override void Exit()
    {
        // Actions.StopEffect?.Invoke(EffectType.Jump);
        // particle.RPC_StopParticle(EffectType.Jump);

    }
}
