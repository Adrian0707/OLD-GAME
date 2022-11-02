
using UnityEngine;
using StateStuff;
using Pathfinding;

public class VilligerLookingForJobWithTool : State<AIViliger>
{


    private GameObject[] objects = null;
    private Vector3 change;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;


    private static VilligerLookingForJobWithTool _instance;
    private VilligerLookingForJobWithTool()

    {
        if (_instance != null)
        {
            //  return;
        }
        _instance = this;
    }
    public static VilligerLookingForJobWithTool Instance
    {
        get
        {
            // if (_instance == null)
            // {
            new VilligerLookingForJobWithTool();
            // }

            return _instance;
        }
    }
    public override void EnterState(AIViliger _owner)
    {
        Fireplace fireplace = GameObject.FindGameObjectWithTag("Fireplace").GetComponent<Fireplace>();
        seeker = _owner.GetComponent<Seeker>();
        // Debug.Log("Looking for job with tool " + _owner.name);
        if (_owner.gameObject.GetComponent<Tool>().Item.name == "Axe")
        {
            //  Debug.Log("axe " + _owner.name);
            objects = fireplace.trees.ToArray();

        }
        if (_owner.gameObject.GetComponent<Tool>().Item.name == "Hammer")
        {
            // Debug.Log("axe " + _owner.name);
            objects = GameObject.FindGameObjectsWithTag("ToBuild");
        }
        if (_owner.gameObject.GetComponent<Tool>().Item.name == "Pick")
        {
            //Debug.Log("axe " + _owner.name);
            objects = fireplace.stones.ToArray();
        }
       
    }

    public override void ExitState(AIViliger _owner)
    {

    }

    public override void UpdateState(AIViliger _owner)
    {
      if (!_owner.corutineIsRunning)
        {
            if (objects.Length > 0)
            {
                GameObject nearest = null;
                foreach (GameObject gObj in objects)
                {

                    if (gObj != null)
                    {
                        if (CanGoToTarget(gObj, _owner))
                        {
                            if (nearest == null)
                            {
                                nearest = gObj;
                            }
                            else if (Vector3.Distance(gObj.transform.position, _owner.transform.position) <
                                Vector3.Distance(nearest.transform.position, _owner.transform.position))
                            {

                                nearest = gObj;

                            }
                        }
                    }
                }
                if (nearest != null)
                {
                    _owner.targetGoTo = nearest;
                    _owner.stateChain.Push(ViligerAttackAnim.Instance);
                    _owner.stateMachine.ChangeState(ViligerGo.Instance);
                }
                else
                {
                    _owner.StartCoroutine(_owner.WaitForS(3));
                    _owner.stateMachine.ChangeState(VilligerLookingForJobWithTool.Instance);
                }

            }
            else
            {
                //Debug.LogError("Nie ma obiektu typu");
                _owner.stateMachine.ChangeState(VilligerLookingForJobWithTool.Instance);
            }
        }
    }

    void UpdatePath(AIViliger _owner)
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, _owner.targetGoTo.transform.position, OnPathComplete);
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
     bool CanGoToTarget(GameObject gObj, AIViliger _owner)
    {
     
        GraphNode node1 = AstarPath.active.GetNearest(gObj.transform.position,NNConstraint.Default).node;
        GraphNode node2 = AstarPath.active.GetNearest(_owner.transform.position, NNConstraint.Default).node;
        //  Debug.LogError(PathUtilities.IsPathPossible(node1, node2));
        // Debug.LogError(gObj.name);
        if (node1 != null && node2 != null)
        {
            if (PathUtilities.IsPathPossible(node1, node2))
            {

                Path p = seeker.StartPath(gObj.transform.position, _owner.transform.position);
                p.BlockUntilCalculated();
                // Debug.LogError(PathUtilities.IsPathPossible(node1, node2));
                return true;

            }
        }
        return false;
    }
}



