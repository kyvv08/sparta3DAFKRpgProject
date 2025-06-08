using System;
using UnityEngine;

[Serializable]
public class PlayerGroundData
{
    [field: SerializeField][field:Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; private set; } = 1f;

    [field:Header("IdleData")]

    [field:Header("WalkData")]
    [field: SerializeField][field: Range(0f, 2f)] public float WalkSpeedModifier { get; private set; } = 0.225f;

    [field:Header("RunData")]
    [field:SerializeField][field: Range(0f, 2f)] public float RunSpeedModifier { get; private set; } = 1f;
}

[Serializable]
public class PlayerAttackData
{
    [field: SerializeField]
    [field: Range(0f, 1f)]
    public float BaseAttackSpeed { get; private set; } = 1f;
    
    [field: Header("AttackData")]
    [field: SerializeField] [field: Range(0f, 2f)] public float AttackDelayModifier { get; private set; } = 0.05f;
    [field: SerializeField] public AnimationClip BaseAttackAnimationClip { get; private set; }


    private int upgradeLevel = 0;
    
    public float CurAttackDelay => BaseAttackSpeed - (AttackDelayModifier * upgradeLevel);

    
}

[CreateAssetMenu(fileName = "Player",menuName = "Characters/Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerGroundData GroundData { get; private set; }
    [field: SerializeField] public PlayerAttackData AttackData { get; private set; }
    
    [field: SerializeField] public float RotateSpeed { get; private set; }
    
}