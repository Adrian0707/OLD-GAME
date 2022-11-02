using System.Collections;
using UnityEngine;

public class OgreChase : LogChaseOptimalization
{
    protected Ogre ogre;
    //protected SpriteRenderer myRenderer;
    private bool attacking;
   // protected Animator anim;

    protected override void Start()
    {
        if (enemyStatistics != null)
        {
            GetComponent<CircleCollider2D>().radius = enemyStatistics.chaseDistance.Value;
        }
        attacking = false;
        base.Start();
        myRenderer = gameObject.transform.parent.GetComponent<SpriteRenderer>();
        ogre = gameObject.transform.parent.GetComponent<Ogre>();
        anim = gameObject.transform.parent.GetComponent<Animator>();
    }
    protected override void Update()
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
            if(Vector3.Distance(target.position, transform.position)> Vector3.Distance(toAttack[i].transform.position, transform.position))
            {
                target = toAttack[i].transform;
            }
        }
        if (toAttack.Count > 0&&ogre.currentState!=EnemyState.stagger)
        {
            //target = toAttack[0].transform;
            if ((Vector3.Distance(target.position, transform.position) > 2.4 && toAttack[0].gameObject.CompareTag(playerTag))
                || (Vector3.Distance(target.position, transform.position) > 1.3 && toAttack[0].gameObject.CompareTag(viligersTag)))
            {

                anim.SetBool("Move", true);
                myRigidbody.bodyType = RigidbodyType2D.Dynamic;
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, ogre.enemyStatistics.speed.Value * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                ogre.ChangeState(EnemyState.walk);
                myRenderer.sortingOrder = -(int)transform.position.y + 3;

            }
            else
            {
                if (ogre.currentState == EnemyState.walk && ogre.currentState != EnemyState.stagger)
                {
                    Vector3 temp = Vector3.MoveTowards(transform.position, target.position, ogre.enemyStatistics.speed.Value * Time.deltaTime);
                    ChangeAnim(temp - transform.position);
                    StartCoroutine(AttackCo());
                }
            }
        }
       else if(ogre.currentState != EnemyState.stagger)
        {
           // ogre.ChangeState(EnemyState.idle);
            anim.SetBool("Move", false);
            myRigidbody.bodyType = RigidbodyType2D.Static;
        }

    }
   /* private void Update(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag(viligersTag) || collision.gameObject.CompareTag(playerTag))&&!attacking)
        {
            
            
        }
       

    }*/
    public IEnumerator AttackCo()
    {
        anim.SetBool("Move", false);
        myRigidbody.bodyType = RigidbodyType2D.Static;
        attacking = true;
        ogre.currentState = EnemyState.atack;
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(0.7f);
        ogre.currentState = EnemyState.walk;
        anim.SetBool("attack", false);
        attacking = false;
        
    }
    new public void ChangeAnim(Vector2 direction)
    {
        anim.SetBool("Move", true);
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
    new public void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }
}


