using UnityEngine;
[System.Serializable]
public class OgreStatisticsSerializable :EnemyStatisticsSerializable
{
    public OgreStatisticsSerializable(OgreStatistics enemyStatistics) : base(enemyStatistics)
    {
    }

    /*   public OgreStatisticsSerializable(OgreStatistics enemyStatistics) : base(enemyStatistics)
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
        OgreStatistics ogreStatistics = ScriptableObject.CreateInstance<OgreStatistics>();
        ogreStatistics.color = new Color(this.color[0], this.color[1], this.color[2]);
        Stat health = new Stat();
        health.BaseValue = this.health;
        ogreStatistics.health = health;
        Stat speed = new Stat();
        speed.BaseValue = this.speed;
        ogreStatistics.speed = speed;
        Stat attack = new Stat();
        attack.BaseValue = this.attack;
        ogreStatistics.attack = attack;
        Stat chaseDistance = new Stat();
        chaseDistance.BaseValue = this.chaseDistance;
        ogreStatistics.chaseDistance = chaseDistance;
        ogreStatistics.lootTable = this.lootTable.ToNonSerializable();
        ogreStatistics.enemyPrefabName = this.enemyPrefabName;
        ogreStatistics.name = this.name;
        ogreStatistics.nightEnemy = this.nightEnemy;
        ogreStatistics.inGame = this.inGame;
        ogreStatistics.power = this.power;
        return ogreStatistics;
    }
}
