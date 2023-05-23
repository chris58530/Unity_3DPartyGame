using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/FallToGround", fileName = "PlayerState_FallToGround")]

public class PlayerState_FallToGround : NetworkPlayerState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateNetwork(NetworkInputData inputData)
    {
        base.UpdateNetwork(inputData);
        if (IsAnimationFinish)
            stateMachine.SwitchState(typeof(PlayerState_Stun));
        if (inputData.IsOpenPressed)
        {
            float value = controller.AngryValue;

            shooter.PowerTrigger = value;

        }

    }

}
