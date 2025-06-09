using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRealStat
{
    public uint CurHP;
}

public class Enemy : MonoBehaviour
{
    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    [field: SerializeField] public EnemySO EnemyData { get; private set; }
    [field: SerializeField] public StatSO EnemyStat { get; private set; }
    [field: SerializeField] private EnemyRealStat enemyRealStat = new EnemyRealStat();

    public Animator Animator { get; private set; }

    public EnemyController EnemyController { get;private set; }
    private EnemyStateMachine stateMachine;

    private void Awake()
    {
        AnimationData.Initialize();
        
        Animator = GetComponentInChildren<Animator>();
        EnemyController = GetComponent<EnemyController>();

        stateMachine = new EnemyStateMachine(this);
        stateMachine.ChangeState(stateMachine.IdleState);
        
        EnemyStat.Init();
        enemyRealStat.CurHP = EnemyStat.BaseStat.MaxHP;
    }

    private void Start()
    {
    }
    
    private void Update()
    {
        stateMachine.Update();
    }

    public void TakeDamage(uint damage)
    {
        if (enemyRealStat.CurHP > damage)
        {
            enemyRealStat.CurHP -= damage;
        }
        else
        {
            enemyRealStat.CurHP = 0;
        }
        if (enemyRealStat.CurHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
