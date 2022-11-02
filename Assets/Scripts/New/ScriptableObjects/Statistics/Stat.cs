using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public enum StatModType
{
    Flat=100,
    PercentAdd=200, 
    PercentMult=300, 
}
[Serializable]
public class Stat 
{
   
    public float BaseValue;
    protected float lastBaseValue = float.MinValue;
    protected bool isDirty = true;
    [SerializeField] protected float _value;
    protected readonly List<StatModifier> statModifiers;
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;
    public Stat(float baseValue)
    {
        BaseValue = baseValue;
     
    }
    public Stat()
    {
       // BaseValue = baseValue;
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }

    // Change the Value property to this
    public virtual float Value
    {
        get
        {
            if (isDirty||BaseValue!=lastBaseValue)
            {
                lastBaseValue = BaseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }

    // Change the AddModifier method


    // And change the RemoveModifier method
 
    

   
    public virtual void AddModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
     }

    public virtual bool RemoveModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            return true;
        }
        return false;
    }
    private bool GetSatsFromSource(StatModifier statModifier, object source)
    {
        return statModifier.Source == source;
    }
     public virtual bool RemoveAllModifiersFromSorce(object source)
    {
        bool didRemove = false;
        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    // Add this method to the CharacterStat class
    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;
            return 0; // if (a.Order == b.Order)
        }



    protected virtual float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentAdd = 0; // This will hold the sum of our "PercentAdd" modifiers

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if (mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.PercentAdd) // When we encounter a "PercentAdd" modifier
            {
                sumPercentAdd += mod.Value; // Start adding together all modifiers of this type

                // If we're at the end of the list OR the next modifer isn't of this type
                if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd; // Multiply the sum with the "finalValue", like we do for "PercentMult" modifiers
                    sumPercentAdd = 0; // Reset the sum back to 0
                }
            }
            else if (mod.Type == StatModType.PercentMult) // Percent renamed to PercentMult
            {
                finalValue *= 1 + mod.Value;
            }
        }

        return (float)Math.Round(finalValue, 4);
    }
}