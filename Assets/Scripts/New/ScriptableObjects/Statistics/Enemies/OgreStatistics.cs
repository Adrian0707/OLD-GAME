using UnityEngine;
[CreateAssetMenu(menuName = "Stats/Enemies/OgreStatistics")]
public class OgreStatistics : EnemyStatistics
{
    public OgreStatistics(Stat health, Stat speed, Stat attack, Stat chaseDistance, LootTable lootTable, Color color) : base(health, speed, attack, chaseDistance, lootTable, color)
    {
    }
 
    public override EnemyStatisticsSerializable ToSerializable()
    {
        OgreStatisticsSerializable ogreStatisticsSerializable = new OgreStatisticsSerializable(this);


        return ogreStatisticsSerializable;
    }
}
