using UnityEngine;
[System.Serializable]
public class GhoustBasicStatisticsSerializable :EnemyStatisticsSerializable
{
    public GhoustBasicStatisticsSerializable(GhoustBasicStatistics enemyStatistics) : base(enemyStatistics)
    {
    }

    /*    public GhoustBasicStatisticsSerializable(GhoustBasicStatistics enemyStatistics) : base(enemyStatistics)
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
        }*/
    public override EnemyStatistics ToNonSerializable()
    {
        GhoustBasicStatistics ghoustBasicStatistics = ScriptableObject.CreateInstance<GhoustBasicStatistics>();
        ghoustBasicStatistics.color = new Color(this.color[0], this.color[1], this.color[2]);
        Stat health = new Stat();
        health.BaseValue = this.health;
        ghoustBasicStatistics.health = health;
        Stat speed = new Stat();
        speed.BaseValue = this.speed;
        ghoustBasicStatistics.speed = speed;
        Stat attack = new Stat();
        attack.BaseValue = this.attack;
        ghoustBasicStatistics.attack = attack;
        Stat chaseDistance = new Stat();
        chaseDistance.BaseValue = this.chaseDistance;
        ghoustBasicStatistics.chaseDistance = chaseDistance;
        ghoustBasicStatistics.lootTable = this.lootTable.ToNonSerializable();
        ghoustBasicStatistics.enemyPrefabName = this.enemyPrefabName;
        ghoustBasicStatistics.nightEnemy = this.nightEnemy;
        ghoustBasicStatistics.name = this.name;
        ghoustBasicStatistics.inGame = this.inGame;
        ghoustBasicStatistics.power = this.power;
        return ghoustBasicStatistics;
    }
}
