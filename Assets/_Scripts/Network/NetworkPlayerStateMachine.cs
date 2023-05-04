using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkPlayerStateMachine : NetworkStateMachine
{
 
    //UTF-8 改成 (Big5) 使用中文註釋
    [SerializeField]
    private NetworkPlayerState[] playerStates;


    NetworkMecanimAnimator animator;
    NetworkPlayerController controller;
    NetworkPlayerInput moveInput;

    private void Awake()
    {
        animator = GetComponentInChildren<NetworkMecanimAnimator>();
        moveInput = GetComponent<NetworkPlayerInput>();
        controller = GetComponent<NetworkPlayerController>();
       
        stateTable = new Dictionary<System.Type, IState>(playerStates.Length);
        foreach (NetworkPlayerState state in playerStates)
        {
            //初始化資料加入state.Initializ()中
            state.Initialize(animator, this, controller, moveInput);
            stateTable.Add(state.GetType(), state);
        }

    }
    
    public override void Spawned()
    {
        SwitchOn(stateTable[typeof(PlayerState_Idle)]);
    }
}
