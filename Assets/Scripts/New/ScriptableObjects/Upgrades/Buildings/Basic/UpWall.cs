using System.Collections.Generic;
using UnityEngine;

public enum WallStat
{
    Rad,
    Health
}
[System.Serializable]
public class WallUpgrde
{
    public int amount;
    public StatModType type;
    public WallStat wallStat;
}
[CreateAssetMenu(menuName = "Upgrades/Buildings/Wall")]
public class UpWall : Upgrade
{
    public Signal2 healWall;
    public Signal2 resizeWall;
    public WallStats wallStatistics;
    public WallUpgrde[] wallUpgrdes;
    private List<StatModifier> statModifiers = new List<StatModifier>();
    public bool activable=false;

    public override void Equip()
    {
        if (activated == false)
        {
            activated = true;
            foreach (var up in wallUpgrdes)
            {
                StatModifier statModifier = new StatModifier(up.amount, up.type, this);
                statModifiers.Add(statModifier);
                switch (up.wallStat)
                {
                    case WallStat.Rad:
                        wallStatistics.Rad.AddModifier(statModifier);
                        resizeWall.Raise();
                        break;
                    case WallStat.Health:
                        wallStatistics.health.AddModifier(statModifier);
                        healWall.Raise();
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
            
            wallStatistics.Rad.RemoveAllModifiersFromSorce(this);
            resizeWall.Raise();
            wallStatistics.health.RemoveAllModifiersFromSorce(this);
            healWall.Raise();
        }
    }
}