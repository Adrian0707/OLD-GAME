using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class LogChase : Chase
{
    public Log log;
    protected SpriteRenderer myRenderer;
    //public EnemyStatistics enemyStatistics ;
    protected Animator anim;


    // Start is called before the first frame update
    protected override void Start()
    {
        if (enemyStatistics != null)
        {
            GetComponent<CircleCollider2D>().radius = enemyStatistics.chaseDistance.Value;
        }
        base.Start();
        myRenderer = gameObject.transform.parent.GetComponent<SpriteRenderer>();
        log = gameObject.transform.parent.GetComponent<Log>();
        anim = gameObject.transform.parent.GetComponent<Animator>();
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        myRigidbody.bodyType = RigidbodyType2D.Kinematic;
        if (collision.gameObject.CompareTag(viligersTag) || collision.gameObject.CompareTag(playerTag))
        {
            target = collision.transform;

            if (target != null && Vector2.Distance(target.position, transform.position) < gameObject.GetComponent<CircleCollider2D>().radius)
            {


                if (log.currentState == EnemyState.idle || log.currentState == EnemyState.walk && log.currentState != EnemyState.stagger)
                {

                    log.ChangeState(EnemyState.walk);
                    anim.SetBool("wakeUp", true);
                    Vector3 temp = Vector3.MoveTowards(transform.position, target.position, log.enemyStatistics.speed.Value* Time.deltaTime);
                    ChangeAnim(temp - transform.position);
                    myRigidbody.MovePosition(temp);
                    myRenderer.sortingOrder = -(int)transform.position.y+2;
                }
            }
            else if (log.currentState != EnemyState.stagger)
            { //Debug.LogError(Vector2.Distance(target.position, transform.position) + "   " + gameObject.GetComponent<CircleCollider2D>().radius);
                myRigidbody.bodyType = RigidbodyType2D.Static;
                anim.SetBool("wakeUp", false);
                log.ChangeState(EnemyState.idle);
            }
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
