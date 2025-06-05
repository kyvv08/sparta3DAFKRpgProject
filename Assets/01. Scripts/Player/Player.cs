using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    [field: SerializeField] public PlayerSO PlayerData { get; private set; }

    public Animator Animator { get; private set; }
    public PlayerController Input { get; private set; }
    public CharacterController Controller { get; private set; }

    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        AnimationData.Initialize();

        Animator = GetComponent<Animator>();
        Input = GetComponent<PlayerController>();
        Controller = GetComponent<CharacterController>();

        stateMachine = new PlayerStateMachine(this);
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update()
    {
        stateMachine.Update();
    }
}
