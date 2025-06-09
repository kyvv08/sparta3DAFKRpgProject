using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState
{
    private EnemyStateMachine enemyStateMachine;

    public EnemyIdleState(EnemyStateMachine stateMachine)
    {
        enemyStateMachine = stateMachine;
    }
    
    public void Enter()
    {
        StartAnimation(enemyStateMachine.Enemy.AnimationData.IdleParameterHash);
    }

    void StartAnimation(int hash)
    {
        enemyStateMachine.Enemy.Animator.SetBool(hash,true);
    }

    void StopAnimation(int hash)
    {
        enemyStateMachine.Enemy.Animator.SetBool(hash,false);
    }

    public void Update()
    {
        if (enemyStateMachine.Enemy.EnemyController.IsPlayerInAttackDistance())
        {
            enemyStateMachine.ChangeState(enemyStateMachine.AttackState);
            return;
        }

        if (enemyStateMachine.Enemy.EnemyController.IsPlayerInDetectDistance())
        {
            enemyStateMachine.Enemy.EnemyController.Rotate();
        }
    }

    public void Exit()
    {
        StopAnimation(enemyStateMachine.Enemy.AnimationData.IdleParameterHash);   
    }
}
