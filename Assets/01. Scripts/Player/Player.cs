using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRealStat
{
    public uint Level;
    public uint CurHP;
    public uint CurMP;
    public uint CurExp;
    public uint AdditionalAttack;
}

public class Player : MonoBehaviour
{
    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    [field: SerializeField] public PlayerSO PlayerData { get; private set; }
    [field: SerializeField] public StatSO PlayerStat { get; private set; }
    [field: SerializeField] public PlayerRealStat playerRealStat { get;private set; }= new PlayerRealStat();

    public Animator Animator { get; private set; }
    public PlayerController PlayerController { get; private set; }

    private PlayerStateMachine stateMachine;

    public Action UpdatePlayerUI;
    
    private void Awake()
    {
        AnimationData.Initialize();
        
        Animator = GetComponentInChildren<Animator>();
        PlayerController = GetComponent<PlayerController>();

        stateMachine = new PlayerStateMachine(this);
        stateMachine.ChangeState(stateMachine.IdleState);
    }
    
    private void Start()
    {
        InitStat();
        UpdatePlayerUI?.Invoke();
    }
    
    private void Update()
    {
        stateMachine.Update();
    }

    private void InitStat()
    {
        PlayerStat.Init();
        playerRealStat.CurHP = PlayerStat.BaseStat.MaxHP;
        playerRealStat.CurMP = PlayerStat.BaseStat.MaxMP;
        playerRealStat.CurExp = 0;  //저장 기능 추가 시 변경 필요
        playerRealStat.Level = PlayerStat.BaseStat.Level;
        playerRealStat.AdditionalAttack = PlayerStat.BaseStat.additionalAttack;
    }
    
    public void TakeDamage(uint damage)
    {
        if (playerRealStat.CurHP > damage)
        {
            playerRealStat.CurHP -= damage;
        }
        else
        {
            playerRealStat.CurHP = 0;
        }
        if (playerRealStat.CurHP <= 0)
        {
            Debug.Log("Player Dead, return to Prev Stage");
        }
        UpdatePlayerUI?.Invoke();
    }

    public void UseHPItem(uint value)
    {
        playerRealStat.CurHP = Math.Min(playerRealStat.CurHP + value, PlayerStat.BaseStat.MaxHP);
        UpdatePlayerUI?.Invoke();
    }
    public void UseMPItem(uint value)
    {
        playerRealStat.CurMP = Math.Min(playerRealStat.CurMP + value, PlayerStat.BaseStat.MaxMP);
        UpdatePlayerUI?.Invoke();
    }
    public void UseExpItem(uint value)
    {
        playerRealStat.CurExp += value;
        CheckPlayerLevelUp();
        UpdatePlayerUI?.Invoke();
    }

    private void CheckPlayerLevelUp()
    {
        bool isLevelUp = false;
        while (playerRealStat.CurExp >= PlayerStat.BaseStat.ExpToNextLevel)
        {
            ++playerRealStat.Level;
            playerRealStat.CurExp -= PlayerStat.BaseStat.ExpToNextLevel;
            isLevelUp = true;
        }

        if (isLevelUp)
        {
            playerRealStat.CurHP = PlayerStat.BaseStat.MaxHP;
            playerRealStat.CurMP = PlayerStat.BaseStat.MaxMP;
        }
    }
}
