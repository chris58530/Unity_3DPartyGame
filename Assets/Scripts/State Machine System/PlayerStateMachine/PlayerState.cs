using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ScriptableObject, IState
{
    [SerializeField]
    private string[] stateName;
    [SerializeField, Range(0f, 1f)]
    private float transitionDuartion = 0.1f;
    int stateHash;
    protected Animator animator;
    protected PlayerStateMachine stateMachine;
    protected PlayerController controller;
    protected PlayerMoveInput moveInput;
    protected bool IsAnimationFinish => StateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;

    protected float StateDuration => Time.time - stateStartTime;
    float stateStartTime;
    string aniString;
    void OnEnable()
    {

    }
    public void Initialize(Animator animator, PlayerStateMachine stateMachine, PlayerMoveInput moveInput, PlayerController playerController)
    {
        this.animator = animator;
        this.stateMachine = stateMachine;
        this.moveInput = moveInput;
        this.controller = playerController;
    }
    public virtual void Enter()
    {
        stateStartTime = Time.time;
        if (animator != null && animator.transform.gameObject.activeSelf)
        {
        //     int aniInt = Random.Range(0, 2);
        //     aniString = stateName[aniInt];
        //     Debug.Log(aniInt);
            stateHash = Animator.StringToHash(stateName[Random.Range(0, stateName.Length)]);

            animator.CrossFade(stateHash, transitionDuartion);
        }
    }
    public virtual void Exit() { }
    public virtual void LogicUpdate() { }
    public virtual void PhysicUpdate() { }
}
