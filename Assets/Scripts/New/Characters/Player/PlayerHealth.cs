public class PlayerHealth : GenericHealth
{

    public Signal2 healthSignal;
    public Signal2 playerDead;

    public override void Damage(float amoutToDamage)
    {
       
            base.Damage(amoutToDamage);
            // maxHealth.RuntimeValue = currentHealth;

            healthSignal.Raise();
            if (currentHealth <= 0)
            {

                playerDead.Raise();
                FullHeal();
            }
        
    }

    public override void FullHeal()
    {
        base.FullHeal();
        healthSignal.Raise();
    }

    public override void Heal(float amountToHeal)
    {
        base.Heal(amountToHeal);
        healthSignal.Raise();
    }
    
}
