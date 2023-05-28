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
        // 如果沒有移動輸入，切換到Idle
        if (!inputData.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        // 如果IsOpenPressed被按下，切換到Rush
        if (inputData.IsOpenPressed)
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
        // 如果IsOpenPressed被按下
        if (inputData.IsOpenPressed)
        {
            // 如果shooter為空，返回
            if (shooter == null) return;
            // 從controller取得玩家的AngryValue，並設定shooter的觸發力度為value
            float value = controller.AngryValue;
            shooter.PowerTrigger = value;
        }

        

    }
    public override void Exit()
    {
    }


}
