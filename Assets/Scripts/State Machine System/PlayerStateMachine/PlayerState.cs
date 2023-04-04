using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ScriptableObject, IState
{
    protected Animator animator;

    protected PlayerStateMachine stateMachine;
    protected PlayerController controller;
    protected PlayerMoveInput moveInput;

    protected float StateDuration => Time.time - stateStartTime;
    float stateStartTime;
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
    }
    public virtual void Exit() { }
    public virtual void LogicUpdate() { }
    public virtual void PhysicUpdate() { }
}
