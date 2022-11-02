[System.Serializable]
public class Loot
{
    public PowerUp thisLoot;
    public int lootChance;
    public int amount;

    public Loot(PowerUp thisLoot, int lootChance)
    {
        this.thisLoot = thisLoot;
        this.lootChance = lootChance;
    }

    public Loot()
    {
    }
}
