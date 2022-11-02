using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public List<Upgrade> charactersUpgrades;
    public List<Upgrade> buildingsUpgrades;
    public List<Upgrade> worldUpgrades;
    
    public Upgrade FindUpgradeByName(string name)
    {
        List<Upgrade> upgrades = new List<Upgrade>(this.charactersUpgrades);
        upgrades.AddRange(buildingsUpgrades);
        upgrades.AddRange(worldUpgrades);
        foreach (var item in upgrades)
        {
            if (item!=null && item.name == name)
            {
                return item;
            }
        }
        return null;
    }
}
