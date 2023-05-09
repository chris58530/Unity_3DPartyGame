using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkPlayerStateMachine : NetworkStateMachine
{
 
    //UTF-8 改成 (Big5) 使用中文註釋
    [SerializeField]
    private NetworkPlayerState[] playerStates;

[SerializeField]
    public  Animator animator;
    NetworkPlayerController controller;


    private void Awake()
    {
        //這裡會出錯，我才想說不是get本地端的animator
        // animator = GetComponentInChildren<Animator>();
  
        controller = GetComponent<NetworkPlayerController>();
       
        stateTable = new Dictionary<System.Type, IState>(playerStates.Length);
        foreach (NetworkPlayerState state in playerStates)
        {
            //初始化資料加入state.Initializ()中
            state.Initialize(animator, this, controller);
            stateTable.Add(state.GetType(), state);
        }

    }
    
    public override void Spawned()
    {
        SwitchOn(stateTable[typeof(PlayerState_Idle)]);
    }
}
