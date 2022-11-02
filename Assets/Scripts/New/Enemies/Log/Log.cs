public class Log : Enemy
{

    public override void Start()
    {
        base.Start();
        mySprite.color = enemyStatistics.color;
        //health = gameObject.transform.GetComponentInChildren<GenericHealth>();
        // currentState = EnemyState.idle;
    }

    public override void Update()
    {
        base.Update();
        /*if (health.currentHealth <= 0)
        {
            DeathEffect();
            MakeLoot();
            Destroy(gameObject);
        }*/
    }

   
}

    
    

