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
    NetworkInputData inputData;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        stateTable = new Dictionary<System.Type, IState>(playerStates.Length);
        foreach (NetworkPlayerState state in playerStates)
        {
            //初始化資料加入state.Initializ()中
            state.Initialize(animator, this,inputData);
            stateTable.Add(state.GetType(), state);
        }     
        
    }
  
    private void Start()
    {
        SwitchOn(stateTable[typeof(PlayerState_Idle)]);
    }
}
