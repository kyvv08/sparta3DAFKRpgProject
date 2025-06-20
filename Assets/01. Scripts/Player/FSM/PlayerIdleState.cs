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
        //playerStateMachine.Player.Animator.Play(playerStateMachine.Player.AnimationData.IdleParameterHash);
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
        if (IsEnemyAlive())
        {
            playerStateMachine.ChangeState(playerStateMachine.ChasingState);
        }
    }

    public void Exit()
    {
        StopAnimation(playerStateMachine.Player.AnimationData.IdleParameterHash);   
    }

    private bool IsEnemyAlive()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy != null)
        {
            return true;
        }
        return false;
    }
}
