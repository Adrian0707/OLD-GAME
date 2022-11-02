using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LootTableSerializable 
{
    public string name;
    public List<LootSerializable> loots;
    public LootTableSerializable(LootTable lootTable)
    {
        this.name = lootTable.name;
        List<LootSerializable> newLoots= new List<LootSerializable>() ;
        foreach (Loot item in lootTable.loots)
        {
            newLoots.Add(new LootSerializable(item));
        }
        this.loots = newLoots;
    }
    public LootTable ToNonSerializable()
    {
        List<Loot> lootsNonSerializable = new List<Loot>();
        foreach (LootSerializable item in this.loots)
        {
            lootsNonSerializable.Add(item.ToNonSerializable());
        }
        LootTable lootTable = ScriptableObject.CreateInstance<LootTable>();
        lootTable.name = this.name;       
        lootTable.loots = lootsNonSerializable;
        return lootTable;
    }


}
