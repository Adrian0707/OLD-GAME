
using UnityEngine;
using StateStuff;
using Pathfinding;
public class VilligerLookingForTool : State<AIViliger>
{


    private GameObject[] stuff;
    private Vector3 change;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;


    private static VilligerLookingForTool _instance;
    private VilligerLookingForTool()

    {
        if (_instance != null)
        {
            //  return;
        }
        _instance = this;
    }
    public static VilligerLookingForTool Instance
    {
        get
        {
            // if (_instance == null)
            // {
            new VilligerLookingForTool();
            // }

            return _instance;
        }
    }
    public override void EnterState(AIViliger _owner)
    {
        Debug.Log("Looking for tool " + _owner.name);
        stuff = GameObject.FindGameObjectsWithTag("stuff");
        /*foreach (GameObject a in stuff)
        {
            if (a.GetComponent<Tool>().taked != true) { }
                //Brać do lisyt only te które nie są taken żeby ludziki nie latały ja pojebane 
        }*/


    }

    public override void ExitState(AIViliger _owner)
    {
        
    }

    public override void UpdateState(AIViliger _owner)
    {
        if (stuff.Length != 0)
        {
            GameObject nearest = null;
            foreach (GameObject gObj in stuff)
            {
                if (nearest == null)
                {
                    nearest = gObj;
                }
                else if(Vector3.Distance(gObj.transform.position,_owner.transform.position)<
                    Vector3.Distance(nearest.transform.position, _owner.transform.position))
                {
                    nearest = gObj;
                }
            }
            _owner.targetGoTo = nearest;
            _owner.stateChain.Push(ViligerTakeTool.Instance);
            _owner.stateMachine.ChangeState(ViligerGo.Instance);

        }
        else
        {

            _owner.StartCoroutine(_owner.WaitForS(.3f));
           // if (!_owner.corutineIsRunning)
           // {
                _owner.stateMachine.ChangeState(VilligerLookingForTool.Instance);
           // }
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
}



