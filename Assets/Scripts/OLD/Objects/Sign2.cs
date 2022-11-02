using UnityEngine;
using UnityEngine.UI;
public class Sign2 : Interactable {

    
    public GameObject dialogBox;
    public Text dialogText;
    public string text;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(Input.GetButtonDown("attack")&& playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = text;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            context.Raise();
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}
