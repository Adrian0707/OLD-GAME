using System.Collections.Generic;
using UnityEngine;

public enum BlacksmithStat
{
    decreseCosts
}
[System.Serializable]
public class BlacksmithUpgrde
{
    public int amount;
    public StatModType type;
    public BlacksmithStat blacksmithStat;
}
[CreateAssetMenu(menuName = "Upgrades/Buildings/Blacksmith")]
public class UpBlacksmith : Upgrade
{
    public BlacksmithStats blacksmithStatistics;
    public BlacksmithUpgrde[] blacksmithUpgrdes;
    private List<StatModifier> statModifiers = new List<StatModifier>();
    public bool activable=false;

    public override void Equip()
    {
        if (activated == false)
        {
            activated = true;
            // We need to store our modifiers in variables before adding them to the stat.

            //c.health.AddModifier( new StatModifier(0.1f, StatModType.PercentAdd,this));
            foreach (var up in blacksmithUpgrdes)
            {
                StatModifier statModifier = new StatModifier(up.amount, up.type, this);
                statModifiers.Add(statModifier);
                switch (up.blacksmithStat)
                {
                    case BlacksmithStat.decreseCosts:
                        blacksmithStatistics.decreseCosts.AddModifier(statModifier);
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
            // We need to store our modifiers in variables before adding them to the stat.

            //c.health.AddModifier( new StatModifier(0.1f, StatModType.PercentAdd,this));
            //playerStatistics.health.AddModifier(new StatModifier(amount, type, this));

            // Here we need to use the stored modifiers in order to remove them.
            // Otherwise they would be "lost" in the stat forever.
            //c.attack.RemoveModifier(mod1);
            // c.health.RemoveModifier(mod2);
            /*   foreach (var up in playerUpgrdes)
               {
                   switch (up.playerStat)
                   {
                       case PlayerStat.Health:
                           playerStatistics.health.AddModifier(new StatModifier(up.amount, up.type, this));
                           break;
                       case PlayerStat.Speed:
                           playerStatistics.speed.AddModifier(new StatModifier(up.amount, up.type, this));
                           break;
                       case PlayerStat.Attack:
                           playerStatistics.attack.AddModifier(new StatModifier(up.amount, up.type, this));
                           break;
                       default:
                           break;
                   }
               }*/
            /*foreach (var up in playerUpgrdes)
            {
                StatModifier statModifier = new StatModifier(up.amount, up.type, this);
                statModifiers.Add(statModifier);
                switch (up.playerStat)
                {
                    case PlayerStat.Health:
                        playerStatistics.health.RemoveModifier(statModifier);
                        break;
                    case PlayerStat.Speed:
                        playerStatistics.speed.RemoveModifier(statModifier);
                        break;
                    case PlayerStat.Attack:
                        playerStatistics.attack.RemoveModifier(statModifier);
                        break;
                    default:
                        break;
                }
            }*/
              blacksmithStatistics.decreseCosts.RemoveAllModifiersFromSorce(this);
          
        }
    }
}