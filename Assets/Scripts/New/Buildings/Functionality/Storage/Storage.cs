using UnityEngine;

public class Storage : MonoBehaviour
{
    public StorageStats storageStats;
    Gui strategyCanvas;
    public Signal2 noEnoughtResources;
    private void Start()
    {
        strategyCanvas = GameObject.FindWithTag("GUI").GetComponent<Gui>();
    }
    public void SellWood()
    {
        if (strategyCanvas.wood >= 50)
        {
            strategyCanvas.ModifyWood(-50);
            strategyCanvas.ModifyGold((int)storageStats.sellExpensive.Value);
        }
        else
        {
            noEnoughtResources.Raise();
        }
    }
    public int WoodValue()
    {
        return (int)storageStats.sellExpensive.Value;
    }
    public void SellStone()
    {
        if (strategyCanvas.stone >= 30)
        {
            strategyCanvas.ModifyStone(-30);
            strategyCanvas.ModifyGold((int)storageStats.sellExpensive.Value);
        }
        else
        {
            noEnoughtResources.Raise();
        }
    }
    public int StoneValue()
    {
        return (int)storageStats.sellExpensive.Value;
    }
    public void BuyStone()
    {
        if (strategyCanvas.gold >= 2)
        {
            strategyCanvas.ModifyStone((int)storageStats.buyMany.Value);
            strategyCanvas.ModifyGold(-2);
        }
        else
        {
            noEnoughtResources.Raise();
        }
    }
    public int GoldStonePrice()
    {
        return ((int)storageStats.buyMany.Value);
    }
    public void BuyWood()
    {
        if (strategyCanvas.gold >= 1)
        {
            strategyCanvas.ModifyWood((int)storageStats.buyMany.Value);
            strategyCanvas.ModifyGold(-1);
        }
        else
        {
            noEnoughtResources.Raise();
        }
    }
    public int GoldWoodPrice()
    {
        return ((int)storageStats.buyMany.Value);
    }
    public void DayMode()
    {
        transform.Find("Window").gameObject.SetActive(false);
    }
    public void NightMode()
    {
        transform.Find("Window").gameObject.SetActive(true);
    }
}
