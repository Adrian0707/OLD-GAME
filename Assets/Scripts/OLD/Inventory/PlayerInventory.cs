using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "NEW Inventory", menuName = "Inventory/Player Inventory")]

public class PlayerInventory : ScriptableObject
{
    public List<InventoryItem> myInventory = new List<InventoryItem>();
}
