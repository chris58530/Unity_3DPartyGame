using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Fall", fileName = "PlayerState_Fall")]

public class PlayerState_Fall : NetworkPlayerState
{

    [SerializeField]
    private AnimationCurve speedCurve;

    public override void Enter()
    {
        base.Enter();
 
    }

    public override void UpdateNetwork(NetworkInputData inputData)
    {
        base.UpdateNetwork(inputData);
        controller.SetPlayerFallDown(speedCurve.Evaluate(StateDuration));

        if (controller.IsGround)
        {

            // if (controller.IsStun)
            //     stateMachine.SwitchState(typeof(PlayerState_Stun));
            // if (controller.SpeedTime > controller.switchToRush)
            //     stateMachine.SwitchState(typeof(PlayerState_Rush));
            // else
            stateMachine.SwitchState(typeof(PlayerState_Land));
        }
        if (controller.IsStun) return;
        if (controller.IsBall)
        {
            controller.SetPlayerRush(inputData);
        }
        else
        {
            controller.SetPlayerMove(inputData);
        }
        if (inputData.IsLeftPressed)
        {
            if (ability == null) return;
            float value = controller.AngryValue;

            ability.PowerTrigger = value;

        }
    if (inputData.IsLeftPressed)
        {
            controller.IsBall = true;
            stateMachine.SwitchState(typeof(PlayerState_Rush));
        }
    }
}
