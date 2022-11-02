using UnityEngine;
[CreateAssetMenu(menuName = "Stats/Enemies/GhoustBasicStatistics")]
public class GhoustBasicStatistics : EnemyStatistics
{
    
    public GhoustBasicStatistics(Stat health, Stat speed, Stat attack, Stat chaseDistance, LootTable lootTable, Color color) : base(health, speed, attack, chaseDistance, lootTable, color)
    {
    }
    public override EnemyStatisticsSerializable ToSerializable()
    {
        GhoustBasicStatisticsSerializable ghoustBasicStatisticsSerializable = new GhoustBasicStatisticsSerializable(this);
      

        return ghoustBasicStatisticsSerializable;
    }
}
