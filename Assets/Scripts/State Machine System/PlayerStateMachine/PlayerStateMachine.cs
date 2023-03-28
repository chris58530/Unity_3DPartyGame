using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerMoveInput))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(MagnetBody))]

public class PlayerStateMachine : StateMachine
{
    //筆記
    //UTF-8 改成 (Big5) 使用中文註釋
    [SerializeField]
    private PlayerState[] playerStates;

    Animator animator;
    PlayerMoveInput playerMoveInput;
    PlayerController playerController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMoveInput = GetComponent<PlayerMoveInput>();
        playerController = GetComponent<PlayerController>();

        stateTable = new Dictionary<System.Type, IState>(playerStates.Length);
        foreach (PlayerState state in playerStates)
        {
            //初始化資料加入state.Initializ()中
            state.Initialize(animator, this, playerMoveInput, playerController);
            stateTable.Add(state.GetType(), state);
        }


    }
    private void Start()
    {
        SwitchOn(stateTable[typeof(PlayerState_Idle)]);
    }
}
