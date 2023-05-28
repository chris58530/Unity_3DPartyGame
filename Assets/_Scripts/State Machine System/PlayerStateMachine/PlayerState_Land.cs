using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Land", fileName = "PlayerState_Land")]

public class PlayerState_Land : NetworkPlayerState
{
    public override void Enter()
    {
        base.Enter();

    }

    public override void UpdateNetwork(NetworkInputData inputData)
    {
        base.UpdateNetwork(inputData);
        // Debug.Log("stateduration"+StateDuration);
        // Debug.Log("GetCurrentAnimatorStateInfo"+animator.ani.GetCurrentAnimatorStateInfo(0).length);

        // if (inputData.IsJumpPressed)
        // {
        //     stateMachine.SwitchState(typeof(PlayerState_Jump));
        // }

        // if (inputData.Move)
        // {
        //     // if (controller.SpeedTime > controller.switchToRush)
        //     //     stateMachine.SwitchState(typeof(PlayerState_Rush));
        //     // else
        //     stateMachine.SwitchState(typeof(PlayerState_Walk));
        // }
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

}
