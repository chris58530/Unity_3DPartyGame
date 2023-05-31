using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Rush", fileName = "PlayerState_Rush")]

public class PlayerState_Rush : NetworkPlayerState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Rush State");

        controller.SwitchTag("Rush");
        Actions.PlayEffect?.Invoke(EffectType.RushLV1);
        particle.RPC_PlayParticle(EffectType.RushLV1);


        controller.modelCount = 1;
    }
    public override void UpdateNetwork(NetworkInputData inputData)
    {
        base.UpdateNetwork(inputData);
        controller.SetPlayerRush(inputData);


        if (inputData.IsShootPressed)
        {
            controller.IsBall = false;
            stateMachine.SwitchState(typeof(PlayerState_FinsihRush));
        }
        if (inputData.IsJumpPressed)
        {
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }

        if (!controller.IsGround)
        {
            stateMachine.SwitchState(typeof(PlayerState_FinsihRush));
        }
        if (controller.IsStun)
        {
            stateMachine.SwitchState(typeof(PlayerState_FallToGround));
        }
        if (inputData.IsOpenPressed)
        {
            if (shooter == null) return;
            float value = controller.AngryValue;
            shooter.PowerTrigger = value;
        }

    }
    // public override void LogicUpdate()
    // {
    //     if (moveInput.speedtime <= controller.switchToRush)
    //         stateMachine.SwitchState(typeof(PlayerState_FinsihRush));
    //     if (moveInput.Jump)
    //         stateMachine.SwitchState(typeof(PlayerState_Jump));
    //     if (!controller.IsGround)
    //         stateMachine.SwitchState(typeof(PlayerState_Fall));

    //     moveInput.ShowRushSpeed(true);//開啟 speed 文字
    //     if (controller.IsStun)
    //     {
    //         stateMachine.SwitchState(typeof(PlayerState_FallToGround));
    //     }
    // }
    // public override void PhysicUpdate()
    // {
    //     float addItionSpeed = moveInput.speedtime;
    //     controller.SetPlayerAddForce(controller.rushSpeed + addItionSpeed);
    // }
    public override void Exit()
    {
        Actions.StopEffect?.Invoke(EffectType.RushLV1);
        particle.RPC_StopParticle(EffectType.RushLV1);

        controller.SwitchTag("Walk");
        controller.modelCount = 0;
    }

}
