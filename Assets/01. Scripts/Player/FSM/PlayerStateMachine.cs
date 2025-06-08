using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get;private set; }
    
    public Vector2 MovementInput { get; set; }
    public float MoveSpeed { get; private set; }
    public float MoveSpeedModifier { get; set; } = 1f;
    
    public PlayerIdleState IdleState { get;}
    public PlayerChasingState ChasingState { get;}
    public PlayerAttackState AttackState { get; }
    public Transform MainCameraTransform { get; set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        IdleState = new PlayerIdleState(this);
        ChasingState = new PlayerChasingState(this);
        AttackState = new PlayerAttackState(this);
        
        MainCameraTransform = Camera.main.transform;
        MoveSpeed = player.PlayerData.GroundData.BaseSpeed;

        player.PlayerController.SetStateMachine(this);
    }
}
