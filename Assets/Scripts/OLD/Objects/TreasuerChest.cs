using UnityEngine;
using UnityEngine.UI;

public class TreasuerChest : Interactable
{
    [Header("Contents")]
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public BoolValue storedOpen;
    [Header("Signals and Dialogs")]
    public Signal2 raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    [Header("Animations")]
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = storedOpen.RuntimeValue;
        if (isOpen)
        {
            anim.SetBool("opened", true);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("attack") && playerInRange)
        {
            if (!isOpen)
            {
                //open
                OpenChest();
            }
            else
            {
                //chest is opened
                ChestIsAlreadyOpen();
            }
        }
    }
    public void OpenChest()
    {
         //dialog window on
        dialogBox.SetActive(true);
        //dialog text = content test
        dialogText.text = contents.itemDescription;
        //add contents to the inventory
        playerInventory.addItem(contents);
        playerInventory.currentItem = contents;
        //Raise the signal to the player to animate
        raiseItem.Raise();
        //raise the context clue 
        context.Raise();
        //set the chest opened
        isOpen = true;
        anim.SetBool("opened", true);
        storedOpen.RuntimeValue = isOpen;
       
    }
    public void ChestIsAlreadyOpen()
    {
        
        // Dialog off
        dialogBox.SetActive(false);
        //raise the signal to the player to stop animating
        raiseItem.Raise();
       
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = false;

        }
    }
}
