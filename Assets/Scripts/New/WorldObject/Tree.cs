using System.Collections;
using UnityEngine;

public class Tree : GenericHealth
{   public bool taked=false;
    public Sprite dropp;
   // public Sprite cutDown;
    public Signal2 update;
    public GameObject deathEffect;
    public bool inBounds = false;
    public Fireplace fireplace;
    private void Start()
    {
      
    }
    public override void Damage(float amoutToDamage)
    {
        //print(fireplace);
        fireplace = GameObject.FindGameObjectWithTag("Fireplace").GetComponent<Fireplace>();
        if (fireplace.IsInBounds(this.transform)) { inBounds = true; }

        base.Damage(amoutToDamage);
        DamageEffect();
        if (this.currentHealth <= 0)
        {

            /*this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = cutDown;
            this.gameObject.tag = "stump";
            this.GetComponent<Collider2D>().enabled = false;
            this.gameObject.transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
            this.gameObject.transform.Find("ObjectArea").gameObject.SetActive(false);*/
            DeathEffect();
            if (inBounds)
                update.Raise();
        }
       
    } private void DamageEffect()
        {
        StartCoroutine(DamageCo());
        }
    private IEnumerator DamageCo()
    {
        for (int i = -5; i <= 5; i++)
        {
            this.transform.rotation = Quaternion.Euler(0,0,i);
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
    private IEnumerator DeathCo(bool left)
    {
        this.GetComponent<Collider2D>().enabled = false;
        this.gameObject.transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
        this.gameObject.transform.Find("ObjectArea").gameObject.SetActive(false);
        if (left)
        {
            for (int i = 0; i <= 90; i += 2)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, i);
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            for (int i = 0; i >= -90; i -= 2)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, i);
                yield return new WaitForSeconds(0.01f);
            }
        }
        yield return new WaitForSeconds(1f);
        if (inBounds)
        {
            fireplace.trees.Remove(this.gameObject);
        }
        Destroy(this.gameObject);
        //this.transform.rotation.z
    }
    private void DeathEffect()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        effect.GetComponentInChildren<SpriteRenderer>().sortingOrder = this.GetComponentInChildren<SpriteRenderer>().sortingOrder;
        effect.transform.parent = this.transform.parent;
        Destroy(effect, Random.Range(10,100));

        if (deathEffect != null)
        {
            if (Random.Range(0, 2) == 1)
            {
                StartCoroutine(DeathCo(true));
            }
            else
            {
                StartCoroutine(DeathCo(false));
            }
            
        }
    }
}
