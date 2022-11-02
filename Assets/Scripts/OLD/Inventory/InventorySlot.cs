using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI stuff to ")]
    [SerializeField] private TextMeshProUGUI itemNumberText;
    [SerializeField] private Image itemImage;

    [Header("Variables from the item")]
    public InventoryItem thisItem;
    public InventoryManager thisManager;

    public void Setup(InventoryItem newItem, InventoryManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager;
        if (thisItem)
        {
            itemImage.sprite = thisItem.itemImage;
            itemNumberText.text = "" + thisItem.numberHeld;
        }
    }

    public void ClickedOn()
    {
        if (thisItem)
        {
            thisManager.SetupDescriptionAndButton(thisItem.itemDesctiption, thisItem.usable, thisItem);
        }
    }
}
