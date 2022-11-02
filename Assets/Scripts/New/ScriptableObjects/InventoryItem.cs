using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
[CreateAssetMenu(fileName = "NEW Item",menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDesctiption;
    public Sprite itemImage;
    public int numberHeld;
    public bool usable;
    public bool unique;
    public UnityEvent thisEvent;

    public void Use()
    {
       
            thisEvent.Invoke();
     
    }
    public void DecreaseAmount(int amountToDecrease)
    {
        numberHeld-=amountToDecrease;
        if (numberHeld < 0)
        {
            numberHeld = 0;
        }
    }
}
