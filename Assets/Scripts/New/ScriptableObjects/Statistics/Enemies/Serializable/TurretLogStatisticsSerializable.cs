using UnityEngine;
[System.Serializable]
public class TurretLogStatisticsSerializable : EnemyStatisticsSerializable
{
   
    public float shotSpeed;

    public TurretLogStatisticsSerializable(TurretLogStatistics enemyStatistics) : base(enemyStatistics)
    {
        this.shotSpeed = enemyStatistics.shotSpeed.BaseValue;
    }


    /*  public TurretLogStatisticsSerializable(Stat shotSpeed, Stat health, Stat speed, Stat attack, Stat chaseDistance, LootTable lootTable, Color color)
      {
          this.color = new float[2];
          this.shotSpeed = shotSpeed;
          this.health = health;
          this.speed = speed;
          this.attack = attack;
          this.chaseDistance = chaseDistance;
          this.lootTable = lootTable;
          // this.color = color;
          this.color[0] = color.r;
          this.color[1] = color.g;
          this.color[2] = color.b;
      }*/
    /* public TurretLogStatisticsSerializable(TurretLogStatistics enemyStatistics) :base(enemyStatistics)
     {
         this.color = new float[3];
         this.shotSpeed = enemyStatistics.shotSpeed;
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
        TurretLogStatistics turretLogStatistics = ScriptableObject.CreateInstance<TurretLogStatistics>();
        turretLogStatistics.color = new Color(this.color[0], this.color[1], this.color[2]);
        Stat shotSpeed = new Stat();
        shotSpeed.BaseValue = this.shotSpeed;
        turretLogStatistics.shotSpeed = shotSpeed;
        //turretLogStatistics.shotSpeed.BaseValue = this.shotSpeed;
        Stat health = new Stat();
        health.BaseValue = this.health;
        turretLogStatistics.health = health;
        Stat speed = new Stat();
        speed.BaseValue = this.speed;
        turretLogStatistics.speed = speed;
        Stat attack = new Stat();
        attack.BaseValue = this.attack;
        turretLogStatistics.attack = attack;
        Stat chaseDistance = new Stat();
        chaseDistance.BaseValue = this.chaseDistance;
        turretLogStatistics.chaseDistance = chaseDistance;
        turretLogStatistics.lootTable = this.lootTable.ToNonSerializable();
        turretLogStatistics.enemyPrefabName = this.enemyPrefabName;
        turretLogStatistics.name = this.name;
        turretLogStatistics.nightEnemy = this.nightEnemy;
        turretLogStatistics.inGame = this.inGame;
        turretLogStatistics.power = this.power;
        return turretLogStatistics;
    }
}
