using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Idle", fileName = "PlayerState_Idle")]

public class PlayerState_Idle : NetworkPlayerState
{

    public override void Enter()
    {
        base.Enter();
        Debug.Log("idle");
    
    }
    

    public override void UpdateNetwork(NetworkInputData inputData)
    {

        // if (moveInput.Move)//host會控制所有人 其他人不能動
        // {
        //     stateMachine.SwitchState(typeof(PlayerState_Walk));
        // }
        if (inputData.Move)//host可以控制自己 其他人不能動
        {
            stateMachine.SwitchState(typeof(PlayerState_Walk));
        }
        if (inputData.IsJumpPressed)
        {
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }
        if(inputData.IsFirePressed)
        {
            shooter.OpenMagnet();
        }
        if(!inputData.IsFirePressed){
            shooter.ShootMagnet();
        }

      
    }

    public override void Exit()
    {

    }
}
