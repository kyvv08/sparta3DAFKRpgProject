using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerChasingState : IState
{
    private PlayerStateMachine playerStateMachine;

    public PlayerChasingState(PlayerStateMachine stateMachine)
    {
        playerStateMachine = stateMachine;
    }
    
    public void Enter()
    {
        playerStateMachine.MoveSpeedModifier = playerStateMachine.Player.PlayerStat.BaseStat.BaseMoveSpeed;
        StartAnimation(playerStateMachine.Player.AnimationData.RunParameterHash);

        playerStateMachine.Player.PlayerController.SetTarget();
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
        playerStateMachine.Player.PlayerController.MovePlayer();
    }

    public void Exit()
    {
        StopAnimation(playerStateMachine.Player.AnimationData.RunParameterHash);   
    }
}
