using System.Collections.Generic;
using UnityEngine;

public enum TowerStat
{
    FierSpeed,
 Rad,
 ProjectileSpeed,
 ProjectileDamage,
 ProjectileLifetime,
 ProjectileSize,
 ProjectileDurability
}
[System.Serializable]
public class TowerUpgrde
{
    public int amount;
    public StatModType type;
    public TowerStat towerStat;
}
[CreateAssetMenu(menuName = "Upgrades/Buildings/Tower")]
public class UpTower : Upgrade
{
 
    public TowerStats towerStatistics;
    public TowerUpgrde[] towerUpgrdes;
    private List<StatModifier> statModifiers = new List<StatModifier>();
    public bool activable=false;

    public override void Equip()
    {
        if (activated == false)
        {
            activated = true;
            foreach (var up in towerUpgrdes)
            {
                StatModifier statModifier = new StatModifier(up.amount, up.type, this);
                statModifiers.Add(statModifier);
                switch (up.towerStat)
                {
                    case TowerStat.FierSpeed:
                        towerStatistics.FierSpeed.AddModifier(statModifier);
                        break;
                    case TowerStat.Rad:
                        towerStatistics.Rad.AddModifier(statModifier);
                        break;
                    case TowerStat.ProjectileSpeed:
                        towerStatistics.ProjectileSpeed.AddModifier(statModifier);
                        break;
                    case TowerStat.ProjectileDamage:
                        towerStatistics.ProjectileDamage.AddModifier(statModifier);
                        break;
                    case TowerStat.ProjectileLifetime:
                        towerStatistics.ProjectileLifetime.AddModifier(statModifier);
                        break;
                    case TowerStat.ProjectileSize:
                        towerStatistics.ProjectileSize.AddModifier(statModifier);
                        break;
                    case TowerStat.ProjectileDurability:
                        towerStatistics.ProjectileDurability.AddModifier(statModifier);
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
            
            towerStatistics.FierSpeed.RemoveAllModifiersFromSorce(this);
            towerStatistics.Rad.RemoveAllModifiersFromSorce(this);
            towerStatistics.ProjectileDamage.RemoveAllModifiersFromSorce(this);
            towerStatistics.ProjectileDurability.RemoveAllModifiersFromSorce(this);
            towerStatistics.ProjectileLifetime.RemoveAllModifiersFromSorce(this);
            towerStatistics.ProjectileSpeed.RemoveAllModifiersFromSorce(this);
            towerStatistics.ProjectileSize.RemoveAllModifiersFromSorce(this);
            



        }
    }
}