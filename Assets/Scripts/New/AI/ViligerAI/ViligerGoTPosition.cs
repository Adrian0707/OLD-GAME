
using UnityEngine;
using StateStuff;
using Pathfinding;
public class ViligerGoTPosition: State<AIViliger>
{



    private Vector3 change;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    private float nextUpdate = 1f;
    Seeker seeker;
    Rigidbody2D rb;


    private static ViligerGoTPosition _instance;
    private ViligerGoTPosition()

    {
        if (_instance != null)
        {
            //  return;
        }
        _instance = this;
    }
    public static ViligerGoTPosition Instance
    {
        get
        {
            // if (_instance == null)
            // {
            new ViligerGoTPosition();
            // }

            return _instance;
        }
    }
    public override void EnterState(AIViliger _owner)
    {
       // Debug.Log("ViligergGoPosition " + _owner.name + " to " + _owner.target1.name);
        reachedEndOfPath = false;
        currentWaypoint = 0;
        seeker = _owner.GetComponent<Seeker>();
        rb = _owner.GetComponent<Rigidbody2D>();
        //_owner.InvokeRepeating("UpdatePath", 0f, 1f);
        UpdatePath(_owner);

        // _owner.target1.SetPositionAndRotation(_owner.target1.transform.position, Quaternion.identity);


    }

    public override void ExitState(AIViliger _owner)
    {
        //  Debug.Log("Exiting first state "+_owner.name);
        _owner.Animator.SetBool("moving", false);
        _owner.Animator.SetBool("carry", false);
    }

    public override void UpdateState(AIViliger _owner)
    {
        if (Time.time >= nextUpdate)
        {
            //Debug.Log(Time.time + ">=" + nextUpdate);
            // Change the next update (current second+1)
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            // Call your fonction
            UpdatePath(_owner);
        }

        if (path == null)
        {
            // Debug.Log("ViligergGo null path");
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {

            //Debug.Log("ViligergGoPosition end path");
            // if odległość od celu jest mniejsza niż zamierzona else ponownie wystartuj to co jest 
           
                reachedEndOfPath = true;

                _owner.Animator.SetBool("moving", false);
                if (_owner.stateChain.Count > 0)
                {
                    _owner.stateMachine.ChangeState(_owner.stateChain.Pop());
                }
           
            // return;
        }


        if (!reachedEndOfPath)
        {
            // Debug.Log("ViligergGo do path");
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * _owner.viligerStatistics.speed.Value / 20 * Time.deltaTime;
            Walking(force * 1000, _owner);
            rb.MovePosition(new Vector2(rb.transform.position.x, rb.transform.position.y) + force);
            foreach (SpriteRenderer spriteRenderer in _owner.spriteRenderers)
            {
                spriteRenderer.sortingOrder = -(int)_owner.transform.position.y + 2;
            }

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < _owner.nextWaypountDistance)
            {
                currentWaypoint++;
            }
        }




        /*if (_owner.switchState)
        {
            _owner.stateMachine.ChangeState(ViligerGo.Instance);
            _owner.switchState = false;
        }*/
    }

    void UpdatePath(AIViliger _owner)
    {
       // _owner.astar.Scan();
        if (seeker.IsDone())
            seeker.StartPath(rb.position, _owner.targetGoPosition, OnPathComplete);
        //_owner.astar.Scan();
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



