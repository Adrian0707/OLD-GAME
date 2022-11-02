using System.Collections;
using UnityEngine;
public enum EnemyState
{
    idle,
    walk,
    atack,
    stagger
}

public class Enemy : MonoBehaviour
{
    protected GenericHealth health;
    [Header("Death Effects")]
    public GameObject deathEffect;
    [HideInInspector] public float deathEffectDelay = 1f;
    [HideInInspector] public LootTable thisLoot;
    [HideInInspector] protected SpriteRenderer mySprite;
   
    [Header("State Machine")]
    public EnemyState currentState;

    [Header("Enemy stats")]
    public EnemyStatistics enemyStatistics;

    public virtual void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        
        health = GetComponentInChildren<GenericHealth>();
        currentState = EnemyState.idle;
        thisLoot = enemyStatistics.lootTable;
    }
    public virtual void Update()
    {

        if (health.currentHealth <= 0)
        {
            DeathEffect();
            MakeLoot();
            Destroy(this.gameObject);
        }
    }
    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    protected void MakeLoot()
    {
        if (thisLoot != null)
        {
            Loot current = thisLoot.LootPowerUP();
            if (current != null)
            {
                for (int i = 0; i <= current.amount; i++)
                {
                    GameObject gameObject = Instantiate(current.thisLoot.gameObject, transform.position, Quaternion.identity);
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.y + 2;
                   // print(i);
                    Vector3 random = Random.insideUnitCircle.normalized;
                    
                   
                    //Vector3 temp = Vector3.MoveTowards(transform.position, transform.position + random, 10 * Time.deltaTime * Random.Range(1, Mathf.Sqrt(current.amount)));
                    //gameObject.GetComponent<Rigidbody2D>().MovePosition(temp);
                    gameObject.GetComponent<Rigidbody2D>().AddForce(random*Random.Range(1, Mathf.Sqrt(current.amount*30)), ForceMode2D.Force);
                }
            }
        }
    }
    protected void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            effect.GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.y + 2;
            Destroy(effect, deathEffectDelay);
        }
    }


    private void OnEnable()
    {
        currentState = EnemyState.idle;
    }
    public void Knock(Rigidbody2D myRigidbody, float knockTime)
    {
        currentState = EnemyState.stagger;
        StartCoroutine(KnockCo(myRigidbody, knockTime));
      //  currentState = EnemyState.idle;
    }
    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            if (!(this is GhoustBasic))
            {
                StartCoroutine(FlashCo());
            }
            yield return new WaitForSeconds(knockTime);
            
            myRigidbody.velocity = Vector2.zero;
            if (!(this is GhoustBasic))
            {
                myRigidbody.bodyType = RigidbodyType2D.Static;
            }
            else
            {
                myRigidbody.bodyType = RigidbodyType2D.Kinematic;
            }
           
            currentState = EnemyState.idle;
           // myRigidbody.velocity = Vector2.zero;
        }
    }



    private IEnumerator FlashCo()
    {
        int temp = 0;
        Color currentColor = mySprite.color;
        while (temp < 3)
        {
            if(mySprite!=null)
                mySprite.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            if (mySprite != null)
                mySprite.color = currentColor;
            yield return new WaitForSeconds(0.1f);
            temp++;
        }
       
    }
}