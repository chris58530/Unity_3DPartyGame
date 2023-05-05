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
        Debug.Log("Fall");
    }

    public override void UpdateNetwork(NetworkInputData inputData)
    {
        controller.SetPlayerFallDown(speedCurve.Evaluate(StateDuration));
        if (controller.IsGround)
        {
            Debug.Log($"{this.name}:isground");
            if (inputData.SpeedTime > controller.switchToRush)
                stateMachine.SwitchState(typeof(PlayerState_Rush));
            else
                stateMachine.SwitchState(typeof(PlayerState_Walk));

                // stateMachine.SwitchState(typeof(PlayerState_Land));
        }

        if (controller.IsStun)
            return;

        if (inputData.SpeedTime > controller.switchToRush)
        {
            controller.SetPlayerRush(inputData);
        }
        else
        {
            controller.SetPlayerMove(inputData);
        }
    }
    // public override void LogicUpdate()
    // {
    //     if (controller.IsGround)
    //     {
    //         if (moveInput.speedtime > controller.switchToRush)
    //             stateMachine.SwitchState(typeof(PlayerState_Rush));
    //         else
    //             stateMachine.SwitchState(typeof(PlayerState_Land));
    //     }



    // }
    // public override void PhysicUpdate()
    // {

    //     controller.SetPlayerFallDown(speedCurve.Evaluate(StateDuration));
    //     if (controller.IsStun)
    //         return;

    //     if (moveInput.speedtime > controller.switchToRush)
    //     {
    //         controller.SetPlayerAddForce(controller.rushSpeed);
    //     }
    //     else
    //     {
    //         controller.SetPlayerAddForce(controller.walkSpeed);
    //     }

    // }
}
