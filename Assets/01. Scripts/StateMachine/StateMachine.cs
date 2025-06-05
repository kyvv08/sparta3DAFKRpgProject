using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
    protected IState curState;

    public void ChangeState(IState state)
    {
        curState?.Exit();
        curState = state;
        curState?.Enter();
    }
    
    public void Update()
    {
        curState?.Update();
    }
}
