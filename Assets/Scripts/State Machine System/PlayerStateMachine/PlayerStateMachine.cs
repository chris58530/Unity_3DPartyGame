using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerMoveInput))]
[RequireComponent(typeof(PlayerController))]

public class PlayerStateMachine : StateMachine
{
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
        
        foreach(PlayerState state in playerStates)
        {
            state.Initialize(animator, this, playerMoveInput, playerController);
            stateTable.Add(state.GetType(),state);
        }

       
    }
    private void Start()
    {
        SwitchOn(stateTable[typeof(PlayerState_Idle)]);
    }
}
