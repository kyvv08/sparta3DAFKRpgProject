using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChasingState : IState
{
    private PlayerStateMachine playerStateMachine;
    private Transform playerTransform;
    private Transform target = null;
    
    
    public PlayerChasingState(PlayerStateMachine stateMachine)
    {
        playerStateMachine = stateMachine;
        playerTransform = playerStateMachine.Player.transform;
    }
    
    public void Enter()
    {
        playerStateMachine.MoveSpeedModifier = playerStateMachine.Player.PlayerData.GroundData.RunSpeedModifier;
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
        if (!IsEnemyInAttackDistance())
        {
            playerStateMachine.ChangeState(playerStateMachine.IdleState);
            return;
        }

        if (IsEnemyInAttackDistance())
        {
            playerStateMachine.ChangeState(playerStateMachine.AttackState);
            return;
        }
    }

    public void Exit()
    {
        StopAnimation(playerStateMachine.Player.AnimationData.IdleParameterHash);   
    }

    bool IsEnemyInAttackDistance()
    {
        //적 탐지 코드
        return true;
    }

    bool IsEnemyInDetectDistance()
    {
        //적 탐지 코드
        if (target == null)
        {
            return false;
        }

        return true;
    }
}
