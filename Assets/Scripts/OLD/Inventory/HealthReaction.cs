using UnityEngine;

public class HealthReaction : MonoBehaviour
{
    public FloatValue playerHealth;
    public Signal2 healthSignal;
    public void Use(int amountToIncrease)
    {
        playerHealth.RuntimeValue += amountToIncrease;
        healthSignal.Raise();
    }
}
