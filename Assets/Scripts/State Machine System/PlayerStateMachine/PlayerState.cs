using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ScriptableObject, IState
{
    protected Animator animator;

    protected PlayerStateMachine playerStateMachine;
    protected PlayerController playerController;    
    protected PlayerMoveInput playerMoveInput;
    protected Vector3 currentSpeed;
    protected float currentSpeedX;
    protected float currentSpeedZ;
    public void Initialize(Animator animator,PlayerStateMachine stateMachine,PlayerMoveInput moveInput,PlayerController playerController)
    {
        this.animator = animator;
        this.playerStateMachine = stateMachine;
        this.playerMoveInput = moveInput;
        this.playerController = playerController;
    }
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void LogicUpdate() { }   
    public virtual void PhysicUpdate() { }  
}
