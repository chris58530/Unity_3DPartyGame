using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Idle", fileName = "PlayerState_Idle")]
public class PlayerState_Idle : NetworkPlayerState
{

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateNetwork()
    {
       
      if(Input.GetKey(KeyCode.S)){
        stateMachine.SwitchState(typeof(PlayerState_Walk));
      }
      
    }
   
    public override void Exit()
    {

    }
}
