[System.Serializable]
public class LootSerializable
{
    public PowerUpSerializable thisLoot;
    public int lootChance;
    public int amount;
    public LootSerializable(Loot loot)
    {
        this.thisLoot = new PowerUpSerializable( loot.thisLoot);
        this.lootChance = loot.lootChance;
        this.amount = loot.amount;
    }
    public Loot ToNonSerializable()
    {
        return new Loot(this.thisLoot.ToNonSerializable(),this.lootChance);
    }
}
