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
            float value = controller.AngryValue;

            shooter.PowerTrigger = value;

        }

    }
  
}
