using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyGroundData
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
public class EnemyAttackData
{
    [field: SerializeField]
    [field: Range(0f, 1f)]
    public float BaseAttackSpeed { get; private set; } = 1f;
    
    [field: Header("AttackData")]
    [field: SerializeField] public AnimationClip BaseAttackAnimationClip { get; private set; }
}

[CreateAssetMenu(fileName = "Enemy",menuName = "Characters/Enemy")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField] public EnemyGroundData GroundData { get; private set; }
    [field: SerializeField] public EnemyAttackData AttackData { get; private set; }

    //[field: SerializeField] public StatSO PlayerStat { get; private set; }
    
}
