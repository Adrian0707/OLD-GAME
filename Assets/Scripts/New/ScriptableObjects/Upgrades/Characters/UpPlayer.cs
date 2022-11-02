using System.Collections.Generic;
using UnityEngine;

public enum PlayerStat
{
Health,
Speed,
Attack,
stoneGetting,
stoneDamage,
woodGetting,
woodDamage,
mana
}
[System.Serializable]
public class PlayerUpgrde
{
    public int amount;
    public StatModType type;
    public PlayerStat playerStat;
}
[CreateAssetMenu(menuName = "Upgrades/Player")]
public class UpPlayer : Upgrade
{
    public PlayerStatistics playerStatistics;
    public PlayerUpgrde[] playerUpgrdes;
    private List<StatModifier> statModifiers = new List<StatModifier>();
    public bool activable=false;
    public Signal2 manaUpdate;

    public override void Equip()
    {
        if (activated == false)
        {
            activated = true;
            // We need to store our modifiers in variables before adding them to the stat.

            //c.health.AddModifier( new StatModifier(0.1f, StatModType.PercentAdd,this));
            foreach (var up in playerUpgrdes)
            {
                StatModifier statModifier = new StatModifier(up.amount, up.type, this);
                statModifiers.Add(statModifier);
                switch (up.playerStat)
                {
                    case PlayerStat.Health:
                        playerStatistics.health.AddModifier(statModifier);
                        break;
                    case PlayerStat.Speed:
                        playerStatistics.speed.AddModifier(statModifier);
                        break;
                    case PlayerStat.Attack:
                        playerStatistics.attack.AddModifier(statModifier);
                        break;
                    case PlayerStat.stoneGetting:
                        playerStatistics.stoneGetting.AddModifier(statModifier);
                        break;
                    case PlayerStat.stoneDamage:
                        playerStatistics.stoneDamage.AddModifier(statModifier);
                        break;
                    case PlayerStat.woodGetting:
                        playerStatistics.woodGetting.AddModifier(statModifier);
                        break;
                    case PlayerStat.woodDamage:
                        playerStatistics.woodDamage.AddModifier(statModifier);
                        break;
                    case PlayerStat.mana:
                        playerStatistics.mana.AddModifier(statModifier);
                        manaUpdate.Raise();
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
              playerStatistics.health.RemoveAllModifiersFromSorce(this);
              playerStatistics.attack.RemoveAllModifiersFromSorce(this);
              playerStatistics.speed.RemoveAllModifiersFromSorce(this);
            playerStatistics.stoneDamage.RemoveAllModifiersFromSorce(this);
            playerStatistics.stoneGetting.RemoveAllModifiersFromSorce(this);
            playerStatistics.woodDamage.RemoveAllModifiersFromSorce(this);
            playerStatistics.woodGetting.RemoveAllModifiersFromSorce(this); 
            playerStatistics.mana.RemoveAllModifiersFromSorce(this);
            manaUpdate.Raise();

        }
    }
}