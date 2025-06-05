using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerIdleState : IState
{
    private PlayerStateMachine playerStateMachine;

    public PlayerIdleState(PlayerStateMachine stateMachine)
    {
        playerStateMachine = stateMachine;
    }
    
    public void Enter()
    {
        playerStateMachine.MoveSpeedModifier = 0f;
        StartAnimation(playerStateMachine.Player.AnimationData.IdleParameterHash);
    }

    void StartAnimation(int hash)
    {
        playerStateMachine.Player.Animator.SetBool(hash,true);
    }

    void StopAnimation(int hash)
    {
        playerStateMachine.Player.Animator.SetBool(hash,false);
    }

    public void Update()
    {
        
    }

    public void Exit()
    {
        StopAnimation(playerStateMachine.Player.AnimationData.IdleParameterHash);   
    }
}
