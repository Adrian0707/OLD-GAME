using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GhoustDamage : GenericDamage
{
    private GameObject deathEffect;
    private float deathEffectDelay;
    private GenericHealth genericHealth;
    private GhoustBasic ghoustBasic;
    protected override void Start()
    {
        base.Start();
        ghoustBasic = gameObject.transform.parent.GetComponentInChildren<GhoustBasic>();
        deathEffect = ghoustBasic.deathEffect;
        deathEffectDelay = ghoustBasic.deathEffectDelay;
        genericHealth = transform.parent.transform.Find("Health").GetComponent<GenericHealth>();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        foreach (string collisionTag in collisionTags)
        {


            if (collision.gameObject.CompareTag(collisionTag) && collision.isTrigger)
            {
                DeathEffect();
                ghoustBasic.thisLoot = null;
                // Destroy(this.transform.parent.gameObject);
                this.GetComponent<KnockBack>().OnTriggerEnter2D(collision);
                //this.GetComponentInParent<Light2D>().
                genericHealth.Damage(genericHealth.currentHealth);
                //print()
            }
        }
    }

    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
        }
    }
}
