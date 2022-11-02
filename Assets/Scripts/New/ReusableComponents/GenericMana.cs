using UnityEngine;

public class GenericMana : MonoBehaviour
{
    public PlayerStatistics statisticsPlayer;
    [SerializeField] private Signal2 manaSignal;
    // public FloatValue maxHealth;
    [SerializeField]public float currentMana; 
    // Start is called before the first frame update
    void Awake()
    {
       
        
            currentMana = statisticsPlayer.mana.Value;
       
    }

    // Update is called once per frameSS
    void Update()
    {
        
    }
    public virtual void AddMana(float amountToAdd)
    {
        currentMana += amountToAdd;
        if (currentMana > statisticsPlayer.mana.Value)
        {
            currentMana = statisticsPlayer.mana.Value;
        }
        manaSignal.Raise();
    }
    public virtual void FullMana()
    {
        currentMana = statisticsPlayer.mana.Value;
        manaSignal.Raise();
    }
    public virtual void DecreseMana(float amoutToDamage)
    {
        currentMana -= amoutToDamage;
        if (currentMana<0)
        {
            currentMana = 0;
        }
        manaSignal.Raise();
    }
    public virtual void noMana()
    {
        currentMana = 0;
        manaSignal.Raise();
    }

    
}
