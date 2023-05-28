using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/FallToGround", fileName = "PlayerState_FallToGround")]

public class PlayerState_FallToGround : NetworkPlayerState
{
    public override void Enter()
    {
        base.Enter();
        controller.IsBall = false;
    }

    public override void UpdateNetwork(NetworkInputData inputData)
    {
        base.UpdateNetwork(inputData);
        if (StateDuration > animator.ani.GetCurrentAnimatorStateInfo(0).length)
            stateMachine.SwitchState(typeof(PlayerState_Stun));
        if (inputData.IsOpenPressed)
        {
            if (shooter == null) return;
            float value = controller.AngryValue;

            shooter.PowerTrigger = value;

        }

    }

}
