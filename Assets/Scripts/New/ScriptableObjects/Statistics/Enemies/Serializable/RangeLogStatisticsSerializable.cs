using UnityEngine;
[System.Serializable]
public class RangeLogStatisticsSerializable : EnemyStatisticsSerializable
{
    public RangeLogStatisticsSerializable(RangeLogStatistics enemyStatistics) : base(enemyStatistics)
    {
    }



    /*  public RangeLogStatisticsSerializable(RangeLogStatistics enemyStatistics, string gameObjectName) : base(enemyStatistics,gameObjectName)
 {
     this.color = new float[3];
     this.health = enemyStatistics.health;
     this.speed = enemyStatistics.speed;
     this.attack = enemyStatistics.attack;
     this.chaseDistance = enemyStatistics.chaseDistance;
     this.lootTable = enemyStatistics.lootTable;
     this.color[0] = enemyStatistics.color.r;
     this.color[1] = enemyStatistics.color.g;
     this.color[2] = enemyStatistics.color.b;
     this.enemyPrefabName = 
 }*/
    public override EnemyStatistics ToNonSerializable()
    {
        //base.ToNonSerializable();
        RangeLogStatistics rangeLogStatistics = ScriptableObject.CreateInstance<RangeLogStatistics>();
        rangeLogStatistics.color = new Color(this.color[0], this.color[1], this.color[2]);
        Stat health = new Stat();
        health.BaseValue = this.health;
        rangeLogStatistics.health = health;
        Stat speed = new Stat();
        speed.BaseValue = this.speed;
        rangeLogStatistics.speed = speed;
        Stat attack = new Stat();
        attack.BaseValue = this.attack;
        rangeLogStatistics.attack = attack;
        Stat chaseDistance = new Stat();
        chaseDistance.BaseValue = this.chaseDistance;
        rangeLogStatistics.chaseDistance = chaseDistance;
        rangeLogStatistics.lootTable = this.lootTable.ToNonSerializable();
        rangeLogStatistics.enemyPrefabName = this.enemyPrefabName;
        rangeLogStatistics.nightEnemy = this.nightEnemy;
        rangeLogStatistics.name = this.name;
        rangeLogStatistics.inGame = this.inGame;
        rangeLogStatistics.power = this.power;
        return rangeLogStatistics;
    }
}
