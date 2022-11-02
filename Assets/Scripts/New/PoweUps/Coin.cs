using UnityEngine;

public class Coin : PowerUp
{
    // public Inventory playerInventory;
    public Gui gui;

    public Coin(string type, int price) : base(type, price)
    {
    }




    void Start()
    {
        //powerUpSignal.Raise();
        name = "Coin";
        gui = GameObject.FindGameObjectWithTag("GUI").GetComponent<Gui>();
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHealth") && collision.isTrigger)
        {

            gui.ModifyCoin(1);
           // powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
