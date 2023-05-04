using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkPlayerState :  ScriptableObject, IState
{
    [SerializeField]
    private string[] stateName;
    [SerializeField, Range(0f, 1f)]
    private float transitionDuartion = 0.1f;
    int stateHash;
    protected NetworkMecanimAnimator animator;
    protected NetworkPlayerStateMachine stateMachine;
    protected NetworkPlayerController controller;
    protected NetworkPlayerInput moveInput;


    protected bool IsAnimationFinish => StateDuration >= animator.Animator.GetCurrentAnimatorStateInfo(0).length;

    protected float StateDuration => Time.time - stateStartTime;
    float stateStartTime;
  
    public void Initialize(NetworkMecanimAnimator animator, NetworkPlayerStateMachine stateMachine,
    NetworkPlayerController controller,NetworkPlayerInput moveInput)
    {
        this.animator = animator;
        this.stateMachine = stateMachine;
        this.controller = controller;
        this.moveInput = moveInput;
    }
    public virtual void Enter()
    {
        stateStartTime = Time.time;
        if (animator != null && animator.transform.gameObject.activeSelf)
        {
            //stateName[Random.Range(0, stateName.Length)] = 隨機抽一個動畫
            stateHash = Animator.StringToHash(stateName[Random.Range(0, stateName.Length)]);
            animator.Animator.CrossFade(stateHash, transitionDuartion);
           
        }
    }
    public virtual void Exit() { }
    public virtual void UpdateNetwork() { }
}