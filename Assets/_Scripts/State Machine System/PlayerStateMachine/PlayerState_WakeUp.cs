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
        if (StateDuration > animator.ani.GetCurrentAnimatorStateInfo(0).length)
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        if (!controller.IsGround)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
      
    }

}
