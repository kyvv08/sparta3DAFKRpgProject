using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : IState
{
    private PlayerStateMachine playerStateMachine;
    private Transform playerTransform;
    private Transform target = null;
    
    
    public PlayerAttackState(PlayerStateMachine stateMachine)
    {
        playerStateMachine = stateMachine;
        playerTransform = playerStateMachine.Player.transform;
    }
    
    public void Enter()
    {
        StartAnimation(playerStateMachine.Player.AnimationData.AttackParameterHash);
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
        //공격 중이라면 콤보 상태로
        //playerStateMachine.Player.PlayerController.AttackEnemy();

    }

    public void Exit()
    {
        StopAnimation(playerStateMachine.Player.AnimationData.AttackParameterHash);   
    }
    
}
