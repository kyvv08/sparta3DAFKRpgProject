using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public CharacterController Controller { get; private set; }

    private EnemyStateMachine enemyStateMachine;

    public Player target { get; private set; }

    private float attackDistance = 1.5f;
    private float detectDistance = 5f;


    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        
        target = FindObjectOfType<Player>();
    }

    public void SetStateMachine(EnemyStateMachine stateMachine)
    {
        this.enemyStateMachine = stateMachine;
    }
    
    public bool IsPlayerInAttackDistance()
    {
        //적 탐지 코드
        if (Vector3.Distance(transform.position, target.transform.position) <= attackDistance)
        {
            return true;
        }

        return false;
    }

    public bool IsPlayerInDetectDistance()
    {
        //적 탐지 코드
        if (Vector3.Distance(transform.position, target.transform.position) <= detectDistance)
        {
            return true;
        }

        return false;
    }
    
    //공격 함수
    public void AttackEnemy()
    {
        if (!IsPlayerInAttackDistance())
        {
            enemyStateMachine.ChangeState(enemyStateMachine.IdleState);
            return;
        }
        target.TakeDamage(enemyStateMachine.Enemy.EnemyStat.BaseStat.BaseAttack);
        Debug.Log("Enemy Attack");
    }

    public void Rotate()
    {
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(target.transform.position - transform.position),
            enemyStateMachine.Enemy.EnemyData.GroundData.BaseRotationDamping * Time.deltaTime
        );
    }
}
