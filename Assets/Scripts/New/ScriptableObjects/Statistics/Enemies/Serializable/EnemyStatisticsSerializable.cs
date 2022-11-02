using UnityEngine;
[System.Serializable]
public class EnemyStatisticsSerializable
{
    public string name;
    public bool nightEnemy;
    public string enemyPrefabName;
    public float health;
    public float speed;
    public float attack;
    public float chaseDistance;
    public LootTableSerializable lootTable;
    public float[] color;
    public bool inGame;
    public int power;
    /* public int GetAttack()
     {
         return (int)attack.Value;
     }*/

    public EnemyStatisticsSerializable(EnemyStatistics enemyStatistics)
    {
        color = new float[3];
        this.health = enemyStatistics.health.BaseValue;
        this.speed = enemyStatistics.speed.BaseValue;
        this.attack = enemyStatistics.attack.BaseValue;
        this.chaseDistance = enemyStatistics.chaseDistance.BaseValue;
        this.lootTable = new LootTableSerializable(enemyStatistics.lootTable);
        this.color[0] = enemyStatistics.color.r;
        this.color[1] = enemyStatistics.color.g;
        this.color[2] = enemyStatistics.color.b;
        this.enemyPrefabName = enemyStatistics.enemyPrefabName;
        this.name = enemyStatistics.name;
        this.nightEnemy = enemyStatistics.nightEnemy;
        this.inGame = enemyStatistics.inGame;
        this.power = enemyStatistics.power;
    }
    public virtual EnemyStatistics ToNonSerializable()
    {
        EnemyStatistics enemyStatistics = ScriptableObject.CreateInstance<EnemyStatistics>();
        enemyStatistics.color = new Color(this.color[0], this.color[1], this.color[2]);
        enemyStatistics.health.BaseValue = this.health;
        enemyStatistics.speed.BaseValue = this.speed;
        enemyStatistics.attack.BaseValue = this.attack;
        enemyStatistics.chaseDistance.BaseValue = this.chaseDistance;
        enemyStatistics.lootTable = this.lootTable.ToNonSerializable();
        enemyStatistics.name = this.name;
        enemyStatistics.enemyPrefabName = this.enemyPrefabName;
        enemyStatistics.nightEnemy = this.nightEnemy;
        enemyStatistics.inGame = this.inGame;
        enemyStatistics.power = this.power;
        return enemyStatistics;
    }






    /* protected override void Awake()
{
    base.Awake();
    speed = new Stat(10);
    attack = new Stat(1);
}*/
}
