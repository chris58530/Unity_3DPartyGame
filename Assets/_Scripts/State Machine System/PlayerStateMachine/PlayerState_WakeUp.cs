using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/WakeUp", fileName = "PlayerState_WakeUp")]

public class PlayerState_WakeUp : NetworkPlayerState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateNetwork(NetworkInputData inputData)
    {
        base.UpdateNetwork(inputData);
        if (animator.IsFinish)
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        if (!controller.IsGround)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
        if (inputData.IsOpenPressed)
        {
            float value = controller.AngryValue;

            shooter.PowerTrigger = value;

        }
    }

}
