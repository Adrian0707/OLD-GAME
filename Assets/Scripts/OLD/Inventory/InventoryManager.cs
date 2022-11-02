using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory info")]
    public PlayerInventory playerInventory;
    [SerializeField] private GameObject blankInvenorySlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject useButton;
    public InventoryItem currrentItem;

    public void SetTextAndButton(string desctiption, bool buttonActive)
    {
        descriptionText.text = desctiption;
        if (buttonActive)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }

    void MakeInventorySlots()
    {
        if (playerInventory)
        {
            for(int i = 0; i < playerInventory.myInventory.Count; i++)
            {
                if (playerInventory.myInventory[i].numberHeld > 0)
                {
                    GameObject temp = Instantiate(blankInvenorySlot, inventoryPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryPanel.transform);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();

                    if (newSlot)
                    {
                        newSlot.Setup(playerInventory.myInventory[i], this);
                    }
                }
            }
        }
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        ClearInventorySlots();
        MakeInventorySlots();
        SetTextAndButton("", false);
    }


    public void SetupDescriptionAndButton(string newDesctiptionString, bool isButtonUsable, InventoryItem newtItem)
    {
        currrentItem = newtItem;
        descriptionText.text = newDesctiptionString;
        useButton.SetActive(isButtonUsable);
    }
    private void ClearInventorySlots()
    {
        for(int i =0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }
    public void UseButtonPressed()
    {
        if (currrentItem)
        {
            currrentItem.Use();
            //Clear All of inventory slots
            ClearInventorySlots();
            //Refill all slots With new umbers
            MakeInventorySlots();
            if (currrentItem.numberHeld == 0)
            {
                SetTextAndButton("", false);
            }
        }
    }
}
