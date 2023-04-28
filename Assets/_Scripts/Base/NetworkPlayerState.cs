using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerState :  ScriptableObject, IState
{
    [SerializeField]
    private string[] stateName;
    [SerializeField, Range(0f, 1f)]
    private float transitionDuartion = 0.1f;
    int stateHash;
    protected Animator animator;
    protected NetworkPlayerStateMachine stateMachine;
    protected NetworkInputData InputData;

    protected bool IsAnimationFinish => StateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;

    protected float StateDuration => Time.time - stateStartTime;
    float stateStartTime;
  
    public void Initialize(Animator animator, NetworkPlayerStateMachine stateMachine,NetworkInputData data)
    {
        this.animator = animator;
        this.stateMachine = stateMachine;
        this.InputData = data;
    }
    public virtual void Enter()
    {
        stateStartTime = Time.time;
        if (animator != null && animator.transform.gameObject.activeSelf)
        {
            //stateName[Random.Range(0, stateName.Length)] = 隨機抽一個動畫
            stateHash = Animator.StringToHash(stateName[Random.Range(0, stateName.Length)]);
            animator.CrossFade(stateHash, transitionDuartion);
        }
    }
    public virtual void Exit() { }
    public virtual void UpdateNetwork() { }
}
