using System.Collections.Generic;
using UnityEngine;

public enum FireplaceStat
{
    maxDistance,
    maxViligers
}
[System.Serializable]
public class FireplaceUpgrde
{
    public int amount;
    public StatModType type;
    public FireplaceStat fireplaceStat;
}
[CreateAssetMenu(menuName = "Upgrades/Buildings/Fireplace")]
public class UpFireplace : Upgrade
{
    public Signal2 makePath;
    public Signal2 updateViligerInfo;
    public FireplaceStats fireplaceStatistics;
    public FireplaceUpgrde[] fireplaceUpgrdes;
    private List<StatModifier> statModifiers = new List<StatModifier>();
    public bool activable=false;

    public override void Equip()
    {
        if (activated == false)
        {
            activated = true;
            foreach (var up in fireplaceUpgrdes)
            {
                StatModifier statModifier = new StatModifier(up.amount, up.type, this);
                statModifiers.Add(statModifier);
                switch (up.fireplaceStat)
                {
                    case FireplaceStat.maxDistance:
                        fireplaceStatistics.maxDistance.AddModifier(statModifier);
                        makePath.Raise();
                        break;
                    case FireplaceStat.maxViligers:
                        fireplaceStatistics.maxViligers.AddModifier(statModifier);
                        updateViligerInfo.Raise();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public override void Unequip()
    {
        if (activated == true)
        {
            activated = false;
            
            fireplaceStatistics.maxDistance.RemoveAllModifiersFromSorce(this);
            fireplaceStatistics.maxViligers.RemoveAllModifiersFromSorce(this);
            makePath.Raise();
            updateViligerInfo.Raise();


        }
    }
}