using System.Collections.Generic;
using UnityEngine;

public class EnemiesSystem : MonoBehaviour
{
    public List<GameObject> normalEnemies;
    public List<GameObject> nightEnemies;
    public List<GameObject> playerNormalEnemiesInGame;
    public List<GameObject> playerNightEnemiesInGame;
    public List<LootTable> playerLootTables;
    public List<GameObject> playerNormalEnemies;
    public List<GameObject> playerNightEnemies;
    public List<EnemyStatistics> playerNormalEnemyStatistics;
    public List<EnemyStatistics> playerNightEnemyStatistics;
    public GameObject FindNightEnemyByName(string name)
    {
        foreach (GameObject item in nightEnemies)
        {
            if (item.name == name)
            {
                return item;
            }
        }
        return null;
    }
    public GameObject FindNormalEnemyByName(string name)
    {
        foreach (GameObject item in normalEnemies)
        {
            if (item.name == name)
            {
                return item;
            }
        }
        return null;
    }
}
