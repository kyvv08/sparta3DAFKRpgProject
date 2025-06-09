using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRealStat
{
    public uint CurHP;
    public uint CurMP;
    public uint CurExp;
    
}

public class Player : MonoBehaviour
{
    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    [field: SerializeField] public PlayerSO PlayerData { get; private set; }
    [field: SerializeField] public StatSO PlayerStat { get; private set; }

    public Animator Animator { get; private set; }
    public PlayerController PlayerController { get; private set; }

    private PlayerStateMachine stateMachine;

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
        Cursor.lockState = CursorLockMode.Locked;
        UIManager.Instance.playerUI.SetHp(100f);
        UIManager.Instance.playerUI.SetMp(100f);
        UIManager.Instance.playerUI.SetExp(0f);
        PlayerStat.Init();
    }
    
    private void Update()
    {
        stateMachine.Update();
    }

    public void TakeDamage(uint damage)
    {
        PlayerStat.BaseStat.CurHP -= damage;
        float percentage = (float)PlayerStat.BaseStat.CurHP / PlayerStat.BaseStat.MaxHP;
        UIManager.Instance.playerUI.SetHp(percentage);
    }
}
