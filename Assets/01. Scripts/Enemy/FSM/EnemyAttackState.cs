using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState
{
    private EnemyStateMachine enemyStateMachine;
    private Transform target;
    
    
    public EnemyAttackState(EnemyStateMachine stateMachine)
    {
        enemyStateMachine = stateMachine;
        target = enemyStateMachine.Enemy.EnemyController.target.transform;
    }
    
    public void Enter()
    {
        StartAnimation(enemyStateMachine.Enemy.AnimationData.AttackParameterHash);
    }

    void StartAnimation(int hash)
    {
        enemyStateMachine.Enemy.Animator.SetBool(hash,true);
        enemyStateMachine.Enemy.Animator.speed = enemyStateMachine.Enemy.EnemyStat.BaseStat.BaseAttackSpeed;
    }

    void StopAnimation(int hash)
    {
        enemyStateMachine.Enemy.Animator.SetBool(hash,false);
    }

    public void Update()
    {
        if (!enemyStateMachine.Enemy.EnemyController.IsPlayerInAttackDistance())
        {
            enemyStateMachine.ChangeState(enemyStateMachine.IdleState);
            return;
        }

        enemyStateMachine.Enemy.EnemyController.Rotate();
    }

    public void Exit()
    {
        StopAnimation(enemyStateMachine.Enemy.AnimationData.AttackParameterHash);   
    }

}
