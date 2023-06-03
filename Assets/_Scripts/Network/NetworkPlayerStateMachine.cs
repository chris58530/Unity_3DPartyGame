using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkPlayerStateMachine : NetworkStateMachine
{

    //UTF-8 改成 (Big5) 使用中文註釋
    [SerializeField]
    private NetworkPlayerState[] playerStates;
    NetworkAnimator animator;
    NetworkPlayerController controller;
    NetworkPlayerAbility ability;
   ParticleManager particle;

    private void Awake()
    {
        controller = GetComponent<NetworkPlayerController>();
        // ability = GetComponentInChildren<NetworkMagnetShooter>();
        ability = null;
        animator = GetComponentInChildren<NetworkAnimator>();
        particle = GetComponentInChildren<ParticleManager>();
        stateTable = new Dictionary<System.Type, IState>(playerStates.Length);
        foreach (NetworkPlayerState state in playerStates)
        {
            //初始化資料加入state.Initializ()中
            state.Initialize(this, animator, controller, ability, particle);
            stateTable.Add(state.GetType(), state);
        }

    }

    public override void Spawned()
    {
        SwitchOn(stateTable[typeof(PlayerState_Idle)]);
    }
}
