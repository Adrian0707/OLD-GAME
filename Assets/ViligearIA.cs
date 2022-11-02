using System.Collections;
using Pathfinding;
using UnityEngine;

public class ViligearIA : MonoBehaviour
{
    public AstarPath astar;
   // public Tree tree;
    public Transform target1;
   // public GameObject target2;
    public float speed=200f;
    public float nextWaypountDistance = 3f;
    private Animator animator;
    private Vector3 change;
    Path path;
    int currentWaypoint =0;
    bool reachedEndOfPath = false;

    Seeker  seeker;
    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 1f);
        animator = GetComponent<Animator>();
       // target1.SetPositionAndRotation(tree.transform.position,Quaternion.identity);


    }
    void UpdatePath()
    {
        if(seeker.IsDone())
        seeker.StartPath(rb.position, target1.transform.position, OnPathComplete);
        astar.Scan();
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            
            animator.SetBool("moving", false);
           
                StartCoroutine(AttackCo());
                
              



           /* if (!target1.position.Equals(target2.transform.position))
            {
                GameObject farest = null;
               foreach (GameObject a in GameObject.FindGameObjectsWithTag("breakable"))
                {
                    if (farest == null)
                    {
                        farest = a;
                    }
                    else
                    {
                        if(Vector3.Distance(this.transform.position,a.transform.position)>
                            (Vector3.Distance(this.transform.position, farest.transform.position)))
                        {
                            farest = a;
                        }
                    }
                }*/
                //NewPath(farest);
            //}
            return;
        }
        
        
        if (!reachedEndOfPath)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;
            Walking(force);
            rb.AddForce(force);
            
            
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypountDistance)
            {
                currentWaypoint++;
            }
        }
        
            
       
        
    }
    private IEnumerator AttackCo()
    {
    int i = 0;
   // while (tree.health >= 0 && i < 100)
   // {

        animator.SetBool("attacking", true);
        yield return null;
        yield return new WaitForSeconds(.3f);

            animator.SetBool("attacking", false);
        
        
        //yield return new WaitForSeconds(.3f);
      //  tree.Attack(1);
            i++;
        yield return new WaitForSeconds(3f);
       // }

    }
    void Walking(Vector2 force)
    {
        change = Vector3.zero;
        change.x = force.x; //Input.GetAxisRaw("Horizontal");
        change.y = force.y; //Input.GetAxisRaw("Vertical");
        change.x = Mathf.Round(change.x);
        change.y = Mathf.Round(change.y);
        animator.SetFloat("moveX", change.x);
        animator.SetFloat("moveY", change.y);
        animator.SetBool("moving", true);
    }
    void NewPath(GameObject targetNew)
    {

        target1 = targetNew.transform;
        currentWaypoint = 0;
        reachedEndOfPath = false;
        animator.SetBool("moving", true);
    }
}
