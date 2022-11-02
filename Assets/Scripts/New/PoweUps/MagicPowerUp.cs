using UnityEngine;

public class MagicPowerUp : PowerUp
{
    //public Inventory playerInventory;
    public GenericMana genericMana;
    public MagicPowerUp(string type, int price) : base(type, price)
    {
    }



    //public float magicValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerHealth")&& collision.isTrigger)
        {
            genericMana.AddMana(1);
           
            Destroy(this.gameObject);
        }
    }
}
