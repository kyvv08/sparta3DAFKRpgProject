using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    public uint MaxValue { get; private set; }
    public uint CurrentValue { get; private set; }

    public event Action OnValueChanged;

    public Stat(uint maxValue)
    {
        MaxValue = maxValue;
        CurrentValue = maxValue;
    }

    public void SetMax(uint value)
    {
        MaxValue = value;
        CurrentValue = (uint)Mathf.Min(CurrentValue, MaxValue);
        OnValueChanged?.Invoke();
    }

    public void SetCurrent(uint value)
    {
        CurrentValue = (uint)Mathf.Clamp(value, 0, MaxValue);
        OnValueChanged?.Invoke();
    }

    public void Increase(uint amount)
    {
        SetCurrent(CurrentValue + amount);
    }

    public void Decrease(uint amount)
    {
        SetCurrent(CurrentValue - amount);
    }

    public float GetPercentage()
    {
        return MaxValue == 0 ? 0f : (float)CurrentValue / MaxValue;
    }

    public bool IsZero()
    {
        return CurrentValue <= 0;
    }
}

[CreateAssetMenu(fileName = "Stat", menuName = "Stat/Stat")]
public class StatSO : ScriptableObject
{
    public Stat Level;
    public Stat Hp;
    public Stat Mp;
    public Stat Exp;
    public Stat AttackPower;
    public Stat AttackSpeed;
    public Stat MoveSpeed;
}