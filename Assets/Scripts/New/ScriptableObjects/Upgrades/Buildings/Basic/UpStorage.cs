using System.Collections.Generic;
using UnityEngine;

public enum StorageStat
{
    buyMany,
    sellExpensive
}
[System.Serializable]
public class StorageUpgrde
{
    public int amount;
    public StatModType type;
    public StorageStat storageStat;
}
[CreateAssetMenu(menuName = "Upgrades/Buildings/Storage")]
public class UpStorage : Upgrade
{
    public StorageStats storageStatistics;
    public StorageUpgrde[] storageUpgrdes;
    private List<StatModifier> statModifiers = new List<StatModifier>();
    public bool activable=false;

    public override void Equip()
    {
        if (activated == false)
        {
            activated = true;
            foreach (var up in storageUpgrdes)
            {
                StatModifier statModifier = new StatModifier(up.amount, up.type, this);
                statModifiers.Add(statModifier);
                switch (up.storageStat)
                {
                    case StorageStat.buyMany:
                        storageStatistics.buyMany.AddModifier(statModifier);
                      
                        break;
                    case StorageStat.sellExpensive:
                        storageStatistics.sellExpensive.AddModifier(statModifier);
                       
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
            
            storageStatistics.buyMany.RemoveAllModifiersFromSorce(this);
            storageStatistics.sellExpensive.RemoveAllModifiersFromSorce(this);


        }
    }
}