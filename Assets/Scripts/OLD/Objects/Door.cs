using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    button
}
public class Door : Interactable
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D phisicsCollider;

   
    private void Update()
    {
        if (Input.GetButtonDown("attack"))
        {
            if (playerInRange && thisDoorType== DoorType.key)
            {
                // Does the player have a key?
                if (playerInventory.numberOfKeys > 0)
                {
                    //remove player key
                    playerInventory.numberOfKeys--;
                    //if so, then call the open method
                    Open();
                }
            }
        }
    }
    public void Open()
    {
        //turn off th door's sprite render
        doorSprite.enabled = false;
        //set open = true
        open = true;
        //turn off the door's box collider
        phisicsCollider.enabled = false;
    }
    public void Close()
    {

    }
}
