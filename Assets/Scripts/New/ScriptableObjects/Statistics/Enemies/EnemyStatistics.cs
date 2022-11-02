using UnityEngine;

public class EnemyStatistics : MovingObjectStatistics, Atacking
{
    // public Stat speed;
    public Stat attack;
    public Stat chaseDistance;
    public LootTable lootTable;
    public Color color;
    public string enemyPrefabName;
    public bool nightEnemy;
    public bool inGame;
    public int power;
    public int GetAttack()
    {
        return (int)attack.Value;
    }

    public EnemyStatistics(Stat health,Stat speed,Stat attack, Stat chaseDistance, LootTable lootTable, Color color)
    {
        this.health = health;
        this.speed = speed;
        this.attack = attack;
        this.chaseDistance = chaseDistance;
        this.lootTable = lootTable;
        this.color = color;
    }
/*    public EnemyStatistics()
    {
        this.health = new Stat();
        this.speed = new Stat();
        this.attack = new Stat();
        this.chaseDistance = new Stat();
        this.lootTable = new LootTable(new List<Loot>());
        this.color = new Color();
    }*/

    public virtual EnemyStatisticsSerializable ToSerializable()
    {
        EnemyStatisticsSerializable enemyStatisticsSerializable = new EnemyStatisticsSerializable(this);


        return enemyStatisticsSerializable;
    }




    /* protected override void Awake()
{
    base.Awake();
    speed = new Stat(10);
    attack = new Stat(1);
}*/
}
