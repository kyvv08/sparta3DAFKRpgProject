using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public PlayerInputHandler InputHandler {get; private set;}
    public CharacterController Controller { get; private set; }
    
    private PlayerStateMachine playerStateMachine;

    //플레이어 이동/공격 관련
    private NavMeshPath path;
    
    private Transform target = null;
    
    private int curCornerIndex;

    private float attackDistance = 1.5f;

    private bool isAttacking = false;
    private Coroutine attackDelayCoroutine = null;

    public Action PlayerStatusChanged;
    private void Awake()
    {
        InputHandler = GetComponent<PlayerInputHandler>();
        Controller = GetComponent<CharacterController>();
        path = new NavMeshPath();
    }

    public void SetStateMachine(PlayerStateMachine playerStateMachine)
    {
        this.playerStateMachine = playerStateMachine;
    }
    
    public void SetTarget()
    {
        
        target = FindClosestEnemy();
        if (target != null)
        {
            UpdatePath();
        }
    }

    public void MovePlayer()
    {
        if (target == null)
        {
            playerStateMachine.ChangeState(playerStateMachine.IdleState);
            path = null;
            return;
        }

        if (Vector3.Distance(transform.position, target.position) <= attackDistance)
        {
            playerStateMachine.ChangeState(playerStateMachine.AttackState);
            return;
        }
        
        
        Vector3 nextCorner = path.corners[curCornerIndex];
        Vector3 dir = (nextCorner - transform.position).normalized;
        
        Controller.Move(dir * (playerStateMachine.MoveSpeedModifier * Time.deltaTime));

        dir.y = 0f;
        if (dir.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(dir),
                playerStateMachine.Player.PlayerData.GroundData.BaseRotationDamping * Time.deltaTime
            );
        }
        
        
        if (Vector3.Distance(transform.position, nextCorner) < 0.3f && curCornerIndex < path.corners.Length - 1)
        {
            ++curCornerIndex;
        }

        if (Time.frameCount % 30 == 0)
        {
            UpdatePath();
        }
    }
    
    private bool IsEnemyInAttackDistance()
    {
        //적 탐지 코드
        if (Vector3.Distance(transform.position, target.position) <= attackDistance)
        {
            return true;
        }

        return false;
    }

    private bool IsEnemyInDetectDistance()
    {
        //적 탐지 코드
        if (target == null)
        {
            return false;
        }
        return true;
    }

    private Transform FindClosestEnemy()
    {
        
        List<GameObject> enemies = StageManager.Instance.GetEnemyList();
        Transform closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float temp = Vector3.Distance(transform.position,enemy.transform.position);
            if (temp < distance)
            {
                closest = enemy.transform;
                distance = temp;
            }
        }
        return closest;
    }

    private void UpdatePath()
    {
        if (target == null)
        {
            return;
        }

        if (NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path))
        {
            curCornerIndex = 0;
        }
    }
    
    //공격 함수
    public void AttackEnemy()
    {
        if (target == null)
        {
            playerStateMachine.ChangeState(playerStateMachine.IdleState);
            return;
        }

        if (Vector3.Distance(transform.position, target.position) > attackDistance)
        {
            playerStateMachine.ChangeState(playerStateMachine.ChasingState);
        }
        else
        {
            if(target.GetComponent<Enemy>().TakeDamage(playerStateMachine.Player.playerRealStat.TotalAttack)){
                playerStateMachine.ChangeState(playerStateMachine.IdleState);
            }
        }
    }
}
