using System.Collections;
using UnityEngine;

public class Stone : GenericHealth
{
    public bool taked = false;
    public Sprite dropp;
   // public Sprite cutDown;
    public Signal2 update;
    public GameObject deathEffect;
    public bool inBounds=false;
    public Fireplace fireplace;
    private void Start()
    {
        
    }
    public override void Damage(float amoutToDamage)
    {
        fireplace = GameObject.FindGameObjectWithTag("Fireplace").GetComponent<Fireplace>();
        // print(fireplace);
        if (fireplace.IsInBounds(this.transform)) { inBounds = true; }

        base.Damage(amoutToDamage);
        DamageEffect();
        if (this.currentHealth <= 0)
        {

            /*  this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = cutDown;
              this.gameObject.tag = "stump";
              this.GetComponent<Collider2D>().enabled = false;
              this.gameObject.transform.GetChild(0).GetComponent<Collider2D>().enabled = false;*/
            DeathEffect();
            if(inBounds)
                update.Raise();
        }

    }
   
    private void DamageEffect()
    {
        StartCoroutine(DamageCo());
    }
    private IEnumerator DamageCo()
    {
        for (int i = -5; i <= 5; i++)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, i);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 5; i >= -5; i--)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, i);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = -5; i < 0; i++)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, i);
            yield return new WaitForSeconds(0.01f);
        }
        //this.transform.rotation.z
    }
    private IEnumerator DeathCo()
    {
        this.GetComponent<Collider2D>().enabled = false;
        this.gameObject.transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
        this.gameObject.transform.Find("ObjectArea").gameObject.SetActive(false);
        for (int i = 30; i >= 0; i--)
        {
            this.transform.localScale = new Vector3(0, 0, i/10);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        if (inBounds)
        {
            fireplace.stones.Remove(this.gameObject);
        }
        Destroy(this.gameObject);

        //this.transform.rotation.z
    }
    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            StartCoroutine(DeathCo());
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            effect.GetComponentInChildren<SpriteRenderer>().sortingOrder = this.GetComponentInChildren<SpriteRenderer>().sortingOrder;
            Destroy(effect, 10);
        }
    }
}

