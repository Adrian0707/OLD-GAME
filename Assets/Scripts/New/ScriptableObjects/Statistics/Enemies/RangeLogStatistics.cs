using UnityEngine;
[CreateAssetMenu(menuName = "Stats/Enemies/LogStatistics")]
public class RangeLogStatistics : EnemyStatistics, Atacking
{
    public RangeLogStatistics(Stat health, Stat speed, Stat attack, Stat chaseDistance, LootTable lootTable, Color color) : base(health, speed, attack, chaseDistance, lootTable, color)
    {

    }

    public override EnemyStatisticsSerializable ToSerializable()
    {
        RangeLogStatisticsSerializable rangeLogStatisticsSerializable = new RangeLogStatisticsSerializable(this);


        return rangeLogStatisticsSerializable;
    }
}
