using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]

public class LootTable : ScriptableObject
{
   // public string name;
    public List<Loot> loots;

    public LootTable(List<Loot> loots)
    {
        this.loots = loots;
    }

    public LootTable(string name, List<Loot> loots)
    {
        this.name = name;
        this.loots = loots;
    }

    public Loot LootPowerUP()
    {
        if (loots.Count != 0)
        {
            int cumProp = 0;
            int currentProp = Random.Range(0, 100);
            for (int i = 0; i < loots.Count; i++)
            {
                cumProp += loots[i].lootChance;
                if (currentProp <= cumProp)
                {
                    return loots[i];
                }
            }
        }
        return null;
    }

   
}
