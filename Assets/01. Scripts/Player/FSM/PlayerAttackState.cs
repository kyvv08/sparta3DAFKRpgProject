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
        Debug.Log("StopAnimation 타이밍");
        playerStateMachine.Player.Animator.SetBool(hash,false);
        playerStateMachine.Player.Animator.StopPlayback();
    }

    public void Update()
    {

    }

    public void Exit()
    {
        StopAnimation(playerStateMachine.Player.AnimationData.AttackParameterHash);   
    }
    
}
