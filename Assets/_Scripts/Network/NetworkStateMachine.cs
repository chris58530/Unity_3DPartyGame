using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class NetworkStateMachine : NetworkBehaviour
{

    IState currentState;
    protected Dictionary<System.Type, IState> stateTable;

    public override void FixedUpdateNetwork()
    {

        if (GetInput(out NetworkInputData inputData))
        {
           currentState.UpdateNetwork( inputData);
        }

    }
    protected void SwitchOn(IState newState)
    {
        currentState = newState;
        currentState.Enter();
    }
    public void SwitchState(IState newState)
    {
        currentState.Exit();
        SwitchOn(newState);
    }
    public void SwitchState(System.Type newType)
    {
        SwitchState(stateTable[newType]);
    }
}
