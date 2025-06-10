using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Stat
{
    public uint Level;
    
    public uint maxHP;
    public uint AdditionalHP;
    
    public uint CurHP;
    
    public uint maxMP;
    public uint AdditionalMP;
    public uint CurMP;

    [Header("경험치")]
    public uint ExpToNextLevel;
    public uint CurExp;
    
    [Header("공격력")]
    public uint BaseAttack;
    public uint additionalAttack;
    
    [Header("이동 속도")]
    public float BaseMoveSpeed;
    public float MoveSpeedModifier;
    [Header("공격 속도")]
    public float BaseAttackSpeed;
    public float AttackSpeedModifier;

    public uint totalAttack => BaseAttack + additionalAttack;
    public uint MaxHP => maxHP + AdditionalHP;
    public uint MaxMP => maxMP + AdditionalMP;
}

[CreateAssetMenu(fileName = "Stat", menuName = "Stat/Stat")]
public class StatSO : ScriptableObject
{
    public Stat BaseStat;

    public void Init()
    {
        BaseStat.CurHP = BaseStat.MaxHP;
        BaseStat.CurMP = BaseStat.MaxMP;
        BaseStat.CurExp = 0;
    }
}