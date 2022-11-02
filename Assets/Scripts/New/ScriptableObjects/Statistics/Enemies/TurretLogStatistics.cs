using UnityEngine;
[CreateAssetMenu(menuName = "Stats/Enemies/TurretLogStatistics")]
public class TurretLogStatistics : EnemyStatistics, Atacking
{
    public Stat shotSpeed;
    public TurretLogStatistics(Stat health, Stat speed, Stat attack, Stat chaseDistance, LootTable lootTable, Color color ,Stat shotSpeed) : base(health, speed, attack, chaseDistance, lootTable, color)
    {
        this.shotSpeed = shotSpeed;
    }

    public override EnemyStatisticsSerializable ToSerializable()
    {
        TurretLogStatisticsSerializable turretLogStatisticsSerializable = new TurretLogStatisticsSerializable(this);


        return turretLogStatisticsSerializable;
    }
}
