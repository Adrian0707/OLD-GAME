using UnityEngine;
[System.Serializable]
public class PowerUpSerializable 
{
   public string name;
    public int price;
  // public int amountToIncrease;

    public PowerUpSerializable(PowerUp powerUp)
    {
        this.price = powerUp.price;
        this.name = powerUp.name;
      //  this.amountToIncrease = powerUp.amountToIncrease;
    }
    public PowerUp ToNonSerializable()
    {

        PowerUp power = GameObject.FindGameObjectWithTag("GameObjectsSystem").GetComponent<GameObjectsSystem>().FindDropItemByName(this.name);
        return power;
    }
}
