using UnityEngine;

public class PhisicalInventoryItem : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem thisItem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&& !collision.isTrigger)
        {
            AddItemToInventory();
            Destroy(this.gameObject);
        }
    }
    void AddItemToInventory()
    {
        if(playerInventory&& thisItem)
        {
            if (playerInventory.myInventory.Contains(thisItem))
            {
                this.thisItem.numberHeld++;
            }
            else
            {
                playerInventory.myInventory.Add(thisItem);
                thisItem.numberHeld += 1;
            }
        }
    }
}
