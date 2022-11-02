using System.Collections.Generic;
using UnityEngine;

public class GhoustChase : Chase
{
    public GhoustBasic ghoustBasic;
    public string buildingsTag;
    public List<GameObject> toAttack;
   // protected SpriteRenderer myRenderer;

    protected override void Start()
    {
        toAttack = new List<GameObject>();
        if (enemyStatistics != null)
        {
            GetComponent<CircleCollider2D>().radius = enemyStatistics.chaseDistance.Value;
        }
        base.Start();
        target = GameObject.FindGameObjectWithTag("Fireplace").transform;
       // myRenderer = gameObject.transform.parent.GetComponent<SpriteRenderer>();
    }
/*    }
    public void Update()
    {
        if (target != null)
        {
            ghoustBasic.ChangeState(EnemyState.walk);
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, enemyStatistics.speed.Value * Time.deltaTime);
            myRigidbody.MovePosition(temp);
        }
        else
        {
            ghoustBasic.ChangeState(EnemyState.atack);
        }
    }*/
    private void Update()
    {
        if (ghoustBasic.currentState != EnemyState.stagger)
        {
            for (var i = toAttack.Count - 1; i > -1; i--)
            {
                if (toAttack[i] == null)
                    toAttack.RemoveAt(i);
            }
            if (toAttack.Count > 0)
            {
                //  myRigidbody.bodyType = RigidbodyType2D.Dynamic;
                // target = toAttack[0].transform;

                Vector3 temp = Vector3.MoveTowards(transform.position, toAttack[0].transform.position, enemyStatistics.speed.Value * Time.deltaTime);
                myRigidbody.MovePosition(temp);
                //  myRenderer.sortingOrder = -(int)transform.position.y + 2;


            }
            else
            {
                if (target != null)
                {
                    ghoustBasic.ChangeState(EnemyState.walk);
                    Vector3 temp = Vector3.MoveTowards(transform.position, target.position, enemyStatistics.speed.Value * Time.deltaTime);
                    myRigidbody.MovePosition(temp);
                }
            }
        }

    }



    public void ChangeTarget(Transform target)
        {
            this.target = target;
        }
   /*     private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(viligersTag) || collision.gameObject.CompareTag(playerTag) || collision.gameObject.CompareTag(buildingsTag))
            {
                ChangeTarget(collision.transform);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(viligersTag) || collision.gameObject.CompareTag(playerTag) || collision.gameObject.CompareTag(buildingsTag))
            {
                ChangeTarget(GameObject.FindGameObjectWithTag("Fireplace").transform);
            }
        }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(viligersTag) || collision.gameObject.CompareTag(playerTag) || collision.gameObject.CompareTag(buildingsTag))
        {
            toAttack.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(viligersTag) || collision.gameObject.CompareTag(playerTag) || collision.gameObject.CompareTag(buildingsTag))
        {
            toAttack.Remove(collision.gameObject);
        }
    }
}


