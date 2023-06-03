using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Walk", fileName = "PlayerState_Walk")]
public class PlayerState_Walk : NetworkPlayerState
{
    public override void Enter()
    {
        base.Enter();
        particle.RPC_PlayParticle(EffectType.Walk);
    }

    public override void UpdateNetwork(NetworkInputData inputData)
    {
        base.UpdateNetwork(inputData);
        controller.SetPlayerMove(inputData);
        // 如果沒有移動輸入，切換到Idle
        if (!inputData.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        // 如果IsOpenPressed被按下，切換到Rush
        if (inputData.IsLeftPressed)
        {
            controller.IsBall = true;
            stateMachine.SwitchState(typeof(PlayerState_Rush));
        }
        // 如果跳躍按鈕被按下，切換到Jump
        if (inputData.IsJumpPressed)
        {
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }
        // 如果玩家不在地面上，切換到Fall
        if (!controller.IsGround)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }

        // 如果玩家被擊暈，切換到FallToGround
        if (controller.IsStun)
        {
            stateMachine.SwitchState(typeof(PlayerState_FallToGround));
        }
 



    }
    public override void Exit()
    {
        particle.RPC_StopParticle(EffectType.Walk);

    }


}
