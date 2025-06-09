using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get;private set; }
    
    public EnemyIdleState IdleState { get;}
    //public PlayerChasingState ChasingState { get;}
    public EnemyAttackState AttackState { get; }

    public EnemyStateMachine(Enemy enemy)
    {
        this.Enemy = enemy;

        IdleState = new EnemyIdleState(this);
        AttackState = new EnemyAttackState(this);

       enemy.EnemyController.SetStateMachine(this);
    }
}
