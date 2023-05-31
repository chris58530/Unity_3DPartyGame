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
    NetworkMagnetShooter shooter;
   ParticleManager particle;

    private void Awake()
    {
        controller = GetComponent<NetworkPlayerController>();
        // shooter = GetComponentInChildren<NetworkMagnetShooter>();
        shooter = null;
        animator = GetComponentInChildren<NetworkAnimator>();
        particle = GetComponent<ParticleManager>();
        stateTable = new Dictionary<System.Type, IState>(playerStates.Length);
        foreach (NetworkPlayerState state in playerStates)
        {
            //初始化資料加入state.Initializ()中
            state.Initialize(this, animator, controller, shooter, particle);
            stateTable.Add(state.GetType(), state);
        }

    }

    public override void Spawned()
    {
        SwitchOn(stateTable[typeof(PlayerState_Idle)]);
    }
}
