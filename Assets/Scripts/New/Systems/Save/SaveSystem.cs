
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;



public static class SaveSystem
{
    public static void SaveEnemy(EnemyStatisticsSerializable enemyStatisticsSerializable)
    {
        List<EnemyStatisticsSerializable> enemyStatisticsSerializables = new List<EnemyStatisticsSerializable>();
        enemyStatisticsSerializables.Add(enemyStatisticsSerializable);
        EnemyStatisticsSerializable saved;
        if (GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>().playerNightEnemyStatistics.Count != 0)
        {
            foreach (EnemyStatistics item in GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>().playerNightEnemyStatistics)
            {
                saved = item.ToSerializable();
                enemyStatisticsSerializables.Add(saved);
            }
        }
        if (GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>().playerNormalEnemyStatistics.Count != 0)
        {
            foreach (EnemyStatistics item in GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>().playerNormalEnemyStatistics)
            {
                saved = item.ToSerializable();
                enemyStatisticsSerializables.Add(saved);
            }
        }
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Enemies.inf";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, enemyStatisticsSerializables);
        stream.Close();
    }
    public static void SaveEnemies()
    {
        List<EnemyStatisticsSerializable> enemyStatisticsSerializables = new List<EnemyStatisticsSerializable>();
        EnemyStatisticsSerializable saved;
        if (GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>().playerNightEnemyStatistics.Count != 0)
        {
            foreach (EnemyStatistics item in GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>().playerNightEnemyStatistics)
            {
                saved = item.ToSerializable();
                enemyStatisticsSerializables.Add(saved);
            }
        }
        if (GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>().playerNormalEnemyStatistics.Count != 0)
        {
            foreach (EnemyStatistics item in GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>().playerNormalEnemyStatistics)
            {
                saved = item.ToSerializable();
                enemyStatisticsSerializables.Add(saved);
            }
        }
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Enemies.inf";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, enemyStatisticsSerializables);
        stream.Close();
    }
    public static void LoadEnemy()
    {
        string path = Application.persistentDataPath + "/Enemies.inf";
        if (File.Exists(path))
        {
            foreach (Transform item in GameObject.FindGameObjectWithTag("EnemiesSystem").transform)
            {
                GameObject.Destroy(item.gameObject);
            } 

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            List<EnemyStatisticsSerializable> enemyStatisticsSerializables = formatter.Deserialize(stream) as List<EnemyStatisticsSerializable>;
            stream.Close();

            List<GameObject> nightEnemyInGame = new List<GameObject>();
            List<EnemyStatistics> nightEnemyNon = new List<EnemyStatistics>();
            List<GameObject> nightEnemyGameObjects = new List<GameObject>();

            List<GameObject> normalEnemyInGame = new List<GameObject>();
            List<EnemyStatistics> normalEnemyNon = new List<EnemyStatistics>();
            List<GameObject> normalEnemyGameObjects = new List<GameObject>();
            foreach (EnemyStatisticsSerializable item in enemyStatisticsSerializables)
            {
                EnemyStatistics enemyStatistics = item.ToNonSerializable();
                GameObject enemy;
                if (item.nightEnemy)
                {
                    nightEnemyNon.Add(enemyStatistics);
                    enemy = GameObject.Instantiate(GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>().FindNightEnemyByName(item.enemyPrefabName));
                    
                }
                else
                {
                    normalEnemyNon.Add(enemyStatistics);
                    enemy = GameObject.Instantiate(GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>().FindNormalEnemyByName(item.enemyPrefabName));
                }
                enemy.GetComponent<Enemy>().enemyStatistics = enemyStatistics;
                enemy.name = item.name;
                enemy.transform.SetParent(GameObject.FindGameObjectWithTag("EnemiesSystem").transform);
                if (item.nightEnemy)
                {
                    nightEnemyGameObjects.Add(enemy);
                    if (item.inGame)
                    {
                        nightEnemyInGame.Add(enemy);
                    }
                }
                else
                {
                    normalEnemyGameObjects.Add(enemy);
                    if (item.inGame)
                    {
                        normalEnemyInGame.Add(enemy);
                    }
                }
            }
            GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>().playerNormalEnemiesInGame = normalEnemyInGame;
            GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>().playerNightEnemiesInGame = nightEnemyInGame;
            GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>().playerNightEnemyStatistics = nightEnemyNon;
            GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>().playerNightEnemies = nightEnemyGameObjects;
            GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>().playerNormalEnemyStatistics = normalEnemyNon;
            GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>().playerNormalEnemies = normalEnemyGameObjects;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }
    public static void SaveLootTables(LootTable loots)
    {
        List<LootTableSerializable> lootTables = new List<LootTableSerializable>();
        lootTables.Add(new LootTableSerializable(loots));
        foreach (var item in GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>().playerLootTables)
        {
            lootTables.Add(new LootTableSerializable(item));
        }
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/lotTables.inf";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream,lootTables);
       stream.Close();
    }
    public static void LoadLootTables()
    {
        string path = Application.persistentDataPath + "/lotTables.inf";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            List<LootTableSerializable> lootTables = formatter.Deserialize(stream) as List<LootTableSerializable>;
            stream.Close();
            List<LootTable> lootTablesNon = new List<LootTable>();
            foreach (var item in lootTables)
            {
                lootTablesNon.Add(item.ToNonSerializable());
            }
            GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>().playerLootTables = lootTablesNon;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }
    public static void SaveUpgrades()
    {
        List<UpgradeSerializable> upgradeSerializables = new List<UpgradeSerializable>();
        UpgradeSerializable saved;
        if (GameObject.FindGameObjectWithTag("UpgradeSystem").GetComponent<UpgradeSystem>().buildingsUpgrades.Count != 0)
        {
            foreach (Upgrade item in GameObject.FindGameObjectWithTag("UpgradeSystem").GetComponent<UpgradeSystem>().buildingsUpgrades)
            {
                if (item != null)
                {
                    saved = item.ToSerializable();
                    upgradeSerializables.Add(saved);
                }
            }
        }
        if (GameObject.FindGameObjectWithTag("UpgradeSystem").GetComponent<UpgradeSystem>().charactersUpgrades.Count != 0)
        {
            foreach (Upgrade item in GameObject.FindGameObjectWithTag("UpgradeSystem").GetComponent<UpgradeSystem>().charactersUpgrades)
            {
                if (item != null)
                {
                    saved = item.ToSerializable();
                    upgradeSerializables.Add(saved);
                }
            }
        }
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Upgrades.inf";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, upgradeSerializables);
        stream.Close();
    }
    public static void LoadUpgrades()
    {
        string path = Application.persistentDataPath + "/Upgrades.inf";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            List<UpgradeSerializable> upgradeSerializables = formatter.Deserialize(stream) as List<UpgradeSerializable>;
            stream.Close();
            List<Upgrade> upgrades = new List<Upgrade>();
            foreach (UpgradeSerializable item in upgradeSerializables)
            {
                Upgrade upgrade = GameObject.FindGameObjectWithTag("UpgradeSystem").GetComponent<UpgradeSystem>().FindUpgradeByName(item.name);
                if (upgrade != null)
                {
                    if (item.bought)
                    {
                        upgrade.bought=true;
                    }
                    else
                    {
                        upgrade.bought = false;
                    }
                    if (item.activated)
                    {
                        upgrade.Equip();
                    }
                    else
                    {
                        upgrade.Unequip();
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }
    public static void SaveCoins()
    {
       
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Coins.inf";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, GameObject.FindGameObjectWithTag("GUI").GetComponent<Gui>().coin);
        stream.Close();
    }
    public static void LoadCoins()
    {
        string path = Application.persistentDataPath + "/Coins.inf";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameObject.FindGameObjectWithTag("GUI").GetComponent<Gui>().coin = (int)formatter.Deserialize(stream);
            stream.Close();
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            GameObject.FindGameObjectWithTag("GUI").GetComponent<Gui>().coin = 0;
        }
    }
    public static int GetLoadCoins()
    {
        string path = Application.persistentDataPath + "/Coins.inf";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            return  (int)formatter.Deserialize(stream);
            stream.Close();
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return 0;
        }
    }
    public static void SaveDays(int currentDays)
    {
        
        if (currentDays > LoadDays())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/Days.inf";
            FileStream stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream,currentDays);
            stream.Close();
        }
    }
    public static int LoadDays()
    {
        string path = Application.persistentDataPath + "/Days.inf";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            int days = (int)formatter.Deserialize(stream);
            stream.Close();
            return days;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return 0;
        }
    }
}
