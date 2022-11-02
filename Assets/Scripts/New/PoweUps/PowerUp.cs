using UnityEngine;

public class PowerUp : MonoBehaviour
{

    // public Signal2 powerUpSignal;
    // public string name;
    //  public int amountToIncrease;
    public int price;

    public PowerUp(string type,int price)
    {
        this.price = price;
        this.name = type;
      //  this.amountToIncrease = amountToIncrease;
    }
}
