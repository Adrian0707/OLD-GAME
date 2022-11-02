/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : LogChase
{
    CircleCollider2D circleCollider2D;
    protected override void OnTriggerStay2D(Collider2D collision)
    {
        
        myRigidbody.bodyType = RigidbodyType2D.Dynamic;
        if (collision.gameObject.CompareTag(viligersTag) || collision.gameObject.CompareTag(playerTag))
        {
            target = collision.transform;

            if (target != null && Vector2.Distance(target.position, transform.position) < gameObject.GetComponent<CircleCollider2D>().radius)
            {


                if (log.currentState == EnemyState.idle || log.currentState == EnemyState.walk && log.currentState != EnemyState.stagger)
                {

                    ChangeState(EnemyState.walk);
                    anim.SetBool("wakeUp", true);
                    Vector3 temp = Vector3.MoveTowards(transform.position, target.position, log.moveSpeed * Time.deltaTime);
                    ChangeAnim(temp - transform.position);
                    myRigidbody.MovePosition(temp);
                    renderer.sortingOrder = -(int)transform.position.y + 2;
                }
            }
            else if (log.currentState != EnemyState.stagger)
            { //Debug.LogError(Vector2.Distance(target.position, transform.position) + "   " + gameObject.GetComponent<CircleCollider2D>().radius);
                myRigidbody.bodyType = RigidbodyType2D.Static;
                anim.SetBool("wakeUp", false);
                ChangeState(EnemyState.idle);
            }
        }



    }
    public override void CheckDistance()
    {
        if ((Vector3.Distance(target.position, transform.position) <= chaseRadius)
            && (Vector3.Distance(target.position, transform.position) > attackRadius))
        {
           
                
            
        }
        
        else if ((Vector3.Distance(target.position, transform.position) <= chaseRadius)
            && (Vector3.Distance(target.position, transform.position) <= attackRadius))
        {
            if ( currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                StartCoroutine(AttackCo());
            }
        }
    }
    public IEnumerator AttackCo()
    {
        currentState = EnemyState.atack;
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(1.5f);
        currentState = EnemyState.walk;
        anim.SetBool("attack", false);
    }
}
*/