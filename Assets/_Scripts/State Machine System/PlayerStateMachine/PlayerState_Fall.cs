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
