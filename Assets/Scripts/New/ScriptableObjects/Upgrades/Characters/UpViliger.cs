using System.Collections.Generic;
using UnityEngine;

public enum ViligerStat
{
Health,
 Speed,
 Capicity,
 GaderingSpeed,
 GaderingEffectivnes
}
[System.Serializable]
public class ViligerUpgrde
{
    public int amount;
    public StatModType type;
    public ViligerStat viligerStat;
}
[CreateAssetMenu(menuName = "Upgrades/Viliger")]
public class UpViliger : Upgrade
{
    public ViligerStatistics viligerStatistics;
    public ViligerUpgrde[] viligerUpgrdes;
    private List<StatModifier> statModifiers = new List<StatModifier>();
    public bool activable = false;

    public override void Equip()
    {
        if (activated == false)
        {
            activated = true;
            // We need to store our modifiers in variables before adding them to the stat.

            //c.health.AddModifier( new StatModifier(0.1f, StatModType.PercentAdd,this));
            foreach (var up in viligerUpgrdes)
            {
                StatModifier statModifier = new StatModifier(up.amount, up.type, this);
                statModifiers.Add(statModifier);
                switch (up.viligerStat)
                {
                    case ViligerStat.Health:
                        viligerStatistics.health.AddModifier(statModifier);
                        break;
                    case ViligerStat.Speed:
                        viligerStatistics.speed.AddModifier(statModifier);
                        break;
                    case ViligerStat.Capicity:
                        viligerStatistics.capicity.AddModifier(statModifier);
                        break;
                    case ViligerStat.GaderingSpeed:
                        viligerStatistics.gaderingSpeed.AddModifier(statModifier);
                        break;
                    case ViligerStat.GaderingEffectivnes:
                        viligerStatistics.gaderingEffectivnes.AddModifier(statModifier);
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
              viligerStatistics.health.RemoveAllModifiersFromSorce(this);
            viligerStatistics.capicity.RemoveAllModifiersFromSorce(this);
            viligerStatistics.speed.RemoveAllModifiersFromSorce(this);
            viligerStatistics.gaderingEffectivnes.RemoveAllModifiersFromSorce(this);
            viligerStatistics.gaderingSpeed.RemoveAllModifiersFromSorce(this);
        }
    }
}