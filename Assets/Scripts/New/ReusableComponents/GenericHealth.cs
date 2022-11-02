using UnityEngine;

public class GenericHealth : MonoBehaviour
{
    public Statistics statistics;
    [SerializeField] public float currentHealth;
    public GameObject damageTakeEffect;
    void Awake()
    {
        if (statistics != null)
        {
            currentHealth = statistics.health.Value;
        }
    }
    void Update()
    {

    }
    public virtual void Heal(float amountToHeal)
    {
        currentHealth += amountToHeal;
        if (currentHealth > statistics.health.Value)
        {
            currentHealth = statistics.health.Value;
        }
    }
    public virtual void FullHeal()
    {
        currentHealth = statistics.health.Value;
    }
    public virtual void Damage(float amoutToDamage)
    {
        currentHealth -= amoutToDamage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        if (damageTakeEffect != null)
        {
            GameObject effect = GameObject.Instantiate(damageTakeEffect, this.transform.position, Quaternion.identity);
            if (effect.GetComponent<SpriteRenderer>() != null)
            {
                effect.GetComponent<SpriteRenderer>().sortingOrder = -(int)this.transform.position.y + 3;
            }
            else
            {
                effect.GetComponentInChildren<SpriteRenderer>().sortingOrder = -(int)this.transform.position.y + 3;
            }
            Destroy(effect, 1f);
        }
    }
    public virtual void InstantDeath()
    {
        currentHealth = 0;
        if (GetComponentInParent<Enemy>() is GhoustBasic)
        {
            GetComponentInParent<Enemy>().thisLoot = null;
        }
    }


}
