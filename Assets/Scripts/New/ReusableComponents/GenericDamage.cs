using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class GenericDamage : MonoBehaviour
{
    [SerializeField] public Statistics statistics;
    [SerializeField] public float damage;
    [SerializeField] protected string[] collisionTags;
    protected virtual void Start()
    {
    /*    if (statistics is Atacking)
        {
            Debug.LogError("im attacking");
            Atacking atacking = (Atacking)statistics;
            damage = atacking.GetAttack();
        }*/
       

    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collisionTag != "" && collisionTag != null)
        {*/
        foreach (string collisionTag in collisionTags)
        {

        
            if (collision.gameObject.CompareTag(collisionTag) && collision.isTrigger)
            {
       //     print(collision.name);
                GenericHealth temp = collision.GetComponent<GenericHealth>();
                if (temp)
                {
                    //print(temp.currentHealth);
                    if (statistics == null|| !(statistics is Atacking))
                    {
                       
                        temp.Damage(damage);
                        //print(damage);
                    }
                    else
                    {
                        Atacking atacking = (Atacking)statistics;
                        temp.Damage(atacking.GetAttack());
                        //print(atacking.GetAttack());
                    }
                //print(damage);
                //print(temp.currentHealth);
                }
            }
        }
        }
    
}
