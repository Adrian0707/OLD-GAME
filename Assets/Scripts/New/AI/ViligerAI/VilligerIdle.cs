
using UnityEngine;
using StateStuff;
using Pathfinding;
public class VilligerIdle : State<AIViliger>
{



    private Vector3 change;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;


    private static VilligerIdle _instance;
    private VilligerIdle()

    {
        if (_instance != null)
        {
            //  return;
        }
        _instance = this;
    }
    public static VilligerIdle Instance
    {
        get
        {
            // if (_instance == null)
            // {
            new VilligerIdle();
            // }

            return _instance;
        }
    }
    public override void EnterState(AIViliger _owner)
    {
       // Debug.Log("ViligerIdle");
        _owner.targetGoTo = null;
        //reachedEndOfPath = false;
        //currentWaypoint = 0;
        //seeker = _owner.GetComponent<Seeker>();
        //rb = _owner.GetComponent<Rigidbody2D>();
        //_owner.InvokeRepeating("UpdatePath", 0f, 1f);
        //_owner.target1 = GameObject.FindGameObjectWithTag("house");
        //UpdatePath(_owner);


        //_owner.target1 = GameObject.FindGameObjectWithTag("house");

    }

    public override void ExitState(AIViliger _owner)
    {
        //Debug.Log("Exiting first state " + _owner.name);
    }

    public override void UpdateState(AIViliger _owner) 
    {

        _owner.targetGoTo = GameObject.FindGameObjectWithTag("Fireplace");
            
        if(_owner.targetGoTo != null)
        {
            _owner.targetGoTo.GetComponent<Fireplace>().viligers.AddFirst(_owner.gameObject);
        }

        /*if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;

            _owner.Animator.SetBool("moving", false);
            _owner.stateMachine.ChangeState(VilligerLookingForTool.Instance);

            // return;
        }


        if (!reachedEndOfPath)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * _owner.speed * Time.deltaTime;
            Walking(force, _owner);
            rb.AddForce(force);


            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < _owner.nextWaypountDistance)
            {
                currentWaypoint++;
            }
        }*/
        // GameObject.FindGameObjectWithTag("house")
    }

    void UpdatePath(AIViliger _owner)
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, _owner.targetGoTo.transform.position, OnPathComplete);
        _owner.astar.Scan();
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    void Walking(Vector2 force, AIViliger _owner)
    {
        change = Vector3.zero;
        change.x = force.x; //Input.GetAxisRaw("Horizontal");
        change.y = force.y; //Input.GetAxisRaw("Vertical");
        change.x = Mathf.Round(change.x);
        change.y = Mathf.Round(change.y);
        _owner.Animator.SetFloat("moveX", change.x);
        _owner.Animator.SetFloat("moveY", change.y);
        _owner.Animator.SetBool("moving", true);
    }
}



