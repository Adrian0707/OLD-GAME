using UnityEngine;

public class MagicReaction : MonoBehaviour
{
    public Inventory playerInventory;
    public Signal2 magicSignal;
  public void Use(int amountToIncrease)
    {
        playerInventory.currentMagic += amountToIncrease;
        magicSignal.Raise();
    }
}
