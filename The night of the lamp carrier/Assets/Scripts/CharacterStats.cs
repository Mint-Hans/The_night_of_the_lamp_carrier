using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("主要统计")]
    public Stat strength;
    public Stat vitality;

    public Stat damage;
    public Stat consumption;
    public Stat maxHealth;
    public Stat maxEnergy;

    public int currentHealth;
    public int currentEnergy;

    public System.Action onHealthChange;
    public System.Action onEnergyChange;

    [Header("能量回复")]
    public int energyRestoredOnDamage = 10;

    public bool isDead {  get; private set; }
    protected virtual void Start()
    {
        currentHealth = GetMaxHealthValue();
        currentEnergy = GetMaxEnergyValue();
    }
    public virtual void DoDamage(CharacterStats _targetStats)
    {
        int totalDamage = damage.GetValue() + strength.GetValue();

        RestoreEnergy(energyRestoredOnDamage);

        _targetStats.TakeDamage(totalDamage);
    }
    public virtual void TakeDamage(int _damage)
    { 
        DecreaseHealthBy(_damage);

        Debug.Log(_damage);
        if (currentHealth <= 0 && !isDead)
            Die();
    }
    protected virtual void DecreaseHealthBy(int _damage)
    {
        currentHealth -= _damage;

        if(onHealthChange != null)
            onHealthChange();
    }
    protected virtual void Die()
    {    
        isDead = true;
    }

    public void KillEntity() => Die();
    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue()+ vitality.GetValue() * 5;
    }

    public virtual void TakeConsumption(int _consumption)
    {
        DecreaseEnergyBy(_consumption);

    }
    protected virtual void DecreaseEnergyBy(int _consumption)
    {
        currentEnergy -= _consumption;

        if (onEnergyChange != null)
            onEnergyChange();
    }
   
    public int GetMaxEnergyValue()
    {
        return maxEnergy.GetValue();
    }

    public virtual void RestoreEnergy(int _amount)
    {
        currentEnergy += _amount;

        if (currentEnergy > GetMaxEnergyValue())
        {
            currentEnergy = GetMaxEnergyValue();
        }

        if (onEnergyChange != null)
            onEnergyChange();
    }
    //屎山到这里为止
}
