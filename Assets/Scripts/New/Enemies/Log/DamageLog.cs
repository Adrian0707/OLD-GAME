using System.Collections;
using UnityEngine;

public class DamageLog : GenericDamage
{
    private Log log;
    private LogChase chase;
   // public int spinTime=2;
    public int spines = 2;
    protected override void Start()
    {
        base.Start();
        log = transform.parent.GetComponent<Log>();
        chase = transform.parent.GetComponentInChildren<LogChase>();
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        GenericHealth temp = collision.GetComponent<GenericHealth>();
        if (temp)
        {
          //  StartCoroutine(CoStag());
        }
        
    }
    IEnumerator CoStag()
    {
        if (log.currentState != EnemyState.stagger)
        {
            yield return new WaitForSeconds(1);
            log.currentState = EnemyState.stagger;
            for (int i = 0; i < spines; i++)
        {
            chase.SetAnimFloat(Vector2.left);
            yield return new WaitForSeconds(0.2f);
            chase.SetAnimFloat(Vector2.up);
            yield return new WaitForSeconds(0.2f);
            chase.SetAnimFloat(Vector2.right);
            yield return new WaitForSeconds(0.2f);
            chase.SetAnimFloat(Vector2.down);
            yield return new WaitForSeconds(0.2f);
        }
        if (log.currentState != EnemyState.idle)
        {
            log.currentState = EnemyState.idle;
        }

    }
}
    }
