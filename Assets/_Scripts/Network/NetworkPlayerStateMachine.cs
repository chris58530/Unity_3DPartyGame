using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerStateMachine : NetworkStateMachine
{
    //筆記
    //UTF-8 改成 (Big5) 使用中文註釋
    [SerializeField]
    private NetworkPlayerState[] playerStates;

    Animator animator;
    NetworkPlayerController controller;
    NetworkPlayerInput moveInput;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
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
