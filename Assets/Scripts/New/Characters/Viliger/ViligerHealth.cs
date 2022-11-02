using UnityEngine;

public class ViligerHealth : GenericHealth
{
    GameObject deadEfect;
    public Signal2 updateViligerInfo; 
    public override void Damage(float amoutToDamage)
    {
        base.Damage(amoutToDamage);
        if (currentHealth <= 0)
        {

            if (this != null)
            {
                if (this.gameObject.transform.parent.GetComponent<AIViliger>().targetGoTo != null)
                {
                    if (this.gameObject.transform.parent.GetComponent<AIViliger>().targetGoTo.GetComponent<Tool>() != null)
                    {
                        this.gameObject.transform.parent.GetComponent<AIViliger>().targetGoTo.GetComponent<Tool>().taked = false;
                    }
                }
                if (deadEfect != null)
                {
                    GameObject effect = GameObject.Instantiate(deadEfect, this.transform.position, Quaternion.identity);
                    //effect.transform.localScale = new Vector3(3, 3, 0);
                    //effect.GetComponent<SpriteRenderer>().color = Color.blue;
                    effect.GetComponent<SpriteRenderer>().sortingOrder = -(int)this.transform.position.y + 2; ;
                    Destroy(effect, 1f);
                }
                Destroy(this.gameObject.transform.parent.gameObject);
                updateViligerInfo.Raise();
            }
        }
    }
}
