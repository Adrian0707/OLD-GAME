[System.Serializable]
public class UpgradeSerializable 
{
    public string name;
    public string type;
    public bool activated = false;
    public bool bought = false;

    public UpgradeSerializable(Upgrade upgrade)
    {
        this.name = upgrade.name;
        this.type = upgrade.GetType().ToString();
        this.activated = upgrade.activated;
        this.bought = upgrade.bought;
    }
   
}
