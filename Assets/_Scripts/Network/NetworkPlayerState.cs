using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkPlayerState : ScriptableObject, IState
{
    [SerializeField]
    private string stateName;
    [SerializeField, Range(0f, 1f)]
    private float transitionDuartion = 0.1f;
    int stateHash;

    protected NetworkAnimator animator;
    protected NetworkPlayerStateMachine stateMachine;
    protected NetworkPlayerController controller;
    protected NetworkMagnetShooter shooter;
    protected float StateDuration;

    protected bool IsAnimationFinish;

    protected float stateStartTime;

    public void Initialize(NetworkPlayerStateMachine stateMachine, NetworkAnimator animator,
    NetworkPlayerController controller, NetworkMagnetShooter shooter)
    {
        this.animator = animator;
        this.stateMachine = stateMachine;
        this.controller = controller;
        this.shooter = shooter;
    }
    public virtual void Enter()
    {
        stateStartTime = Time.time;
        if (animator != null && animator.transform.gameObject.activeSelf)
        {
            //stateName[Random.Range(0, stateName.Length)] = 隨機抽一個動畫
            // stateHash = Animator.StringToHash(stateName);
            // animator.Animator.CrossFade(stateHash, transitionDuartion);
            animator.PlayAnimationString = stateName;
        }

    }
    public virtual void Exit() { }
    public virtual void UpdateNetwork(NetworkInputData inputData)
    {
        StateDuration = Time.time - stateStartTime;
        IsAnimationFinish = animator.IsFinish(stateStartTime);

    }
}
