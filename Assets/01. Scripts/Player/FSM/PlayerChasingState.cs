using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerChasingState : IState
{
    private PlayerStateMachine playerStateMachine;
    private Transform playerTransform;
    private Transform target = null;
    
    private NavMeshPath path;
    private int curCornerIndex;

    private float stopDistance = 1.5f;
    
    public PlayerChasingState(PlayerStateMachine stateMachine)
    {
        playerStateMachine = stateMachine;
        playerTransform = playerStateMachine.Player.transform;
        path = new NavMeshPath();
    }
    
    public void Enter()
    {
        Debug.Log("Enter Chasing State");
        playerStateMachine.MoveSpeedModifier = playerStateMachine.Player.PlayerData.GroundData.RunSpeedModifier;
        StartAnimation(playerStateMachine.Player.AnimationData.RunParameterHash);

        target = FindClosestEnemy();
        if (target != null)
        {
            UpdatePath();
        }
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
        
        if (target == null)
        {
            playerStateMachine.ChangeState(playerStateMachine.IdleState);
            return;
        }

        if (Vector3.Distance(playerTransform.position, target.position) <= stopDistance)
        {
            playerStateMachine.ChangeState(playerStateMachine.AttackState);
            return;
        }

        if (path == null || path.corners.Length == 0) return;

        Vector3 nextCorner = path.corners[curCornerIndex];
        Vector3 dir = (nextCorner - playerTransform.position).normalized;
        
        playerStateMachine.Player.PlayerController.Controller.Move(dir * (playerStateMachine.Player.PlayerData.GroundData.RunSpeedModifier * Time.deltaTime));
        //캐릭터 회전 --> 이동방향으로 돌아가도록
        dir.y = 0f;
        playerStateMachine.Player.transform.rotation = Quaternion.Slerp(
            playerStateMachine.Player.transform.rotation,
            Quaternion.LookRotation(dir),
            playerStateMachine.Player.PlayerData.RotateSpeed * Time.deltaTime
        );
        
        
        if (Vector3.Distance(playerTransform.position, nextCorner) < 0.3f && curCornerIndex < path.corners.Length - 1)
        {
            ++curCornerIndex;
        }

        if (Time.frameCount % 30 == 0)
        {
            UpdatePath();
        }
    }

    public void Exit()
    {
        StopAnimation(playerStateMachine.Player.AnimationData.RunParameterHash);   
    }

    private bool IsEnemyInAttackDistance()
    {
        //적 탐지 코드
        if (Vector3.Distance(playerTransform.position, target.position) <= stopDistance)
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
        //나중에 enemyManager 같은 관리 주체를 통해서 enemy 리스트 받아올 것
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float temp = Vector3.Distance(playerTransform.position,enemy.transform.position);
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

        if (NavMesh.CalculatePath(playerTransform.position, target.position, NavMesh.AllAreas, path))
        {
            Debug.Log("Found Path");
            curCornerIndex = 0;
        }
    }
}
