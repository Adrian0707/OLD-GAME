using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class LogChaseOptimalization : Chase
{
    public Log log;
    protected SpriteRenderer myRenderer;
    protected Animator anim;
    public List<GameObject> toAttack;

    protected override void Start()
    {
        toAttack = new List<GameObject>();
        if (enemyStatistics != null)
        {
            GetComponent<CircleCollider2D>().radius = enemyStatistics.chaseDistance.Value;
        }
        base.Start();
        myRenderer = gameObject.transform.parent.GetComponent<SpriteRenderer>();
        log = gameObject.transform.parent.GetComponent<Log>();
        anim = gameObject.transform.parent.GetComponent<Animator>();
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(viligersTag) || collision.gameObject.CompareTag(playerTag))
        {
            toAttack.Add(collision.gameObject);
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(viligersTag) || collision.gameObject.CompareTag(playerTag))
        {
            toAttack.Remove(collision.gameObject);
        }
    }
    protected virtual void Update()
    {
        for (var i = toAttack.Count - 1; i > -1; i--)
        {
            if (toAttack[i] == null)
                toAttack.RemoveAt(i);
        }
        for (int i = 0; i < toAttack.Count; i++)
        {
            if (target == null)
            {
                target = toAttack[i].transform;
            }
            if (Vector3.Distance(target.position, transform.position) > Vector3.Distance(toAttack[i].transform.position, transform.position))
            {
                target = toAttack[i].transform;
            }
        }
        if (toAttack.Count > 0)
        {
            myRigidbody.bodyType = RigidbodyType2D.Dynamic;
            if (log.currentState == EnemyState.idle || log.currentState == EnemyState.walk)
            {
                log.ChangeState(EnemyState.walk);
                if (!anim.GetBool("wakeUp"))
                    anim.SetBool("wakeUp", true);
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, log.enemyStatistics.speed.Value * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                myRenderer.sortingOrder = -(int)transform.position.y + 4;
            }
        }
        else if (log.currentState != EnemyState.stagger)
        {
            myRigidbody.bodyType = RigidbodyType2D.Static;
            anim.SetBool("wakeUp", false);
            log.ChangeState(EnemyState.idle);
        }
    }
    public void ChangeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }
    public void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }
}
