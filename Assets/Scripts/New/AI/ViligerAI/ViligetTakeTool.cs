
using UnityEngine;
using StateStuff;
using Pathfinding;
public class ViligerTakeTool : State<AIViliger>
{



    private Vector3 change;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;


    private static ViligerTakeTool _instance;
    private ViligerTakeTool()

    {
        if (_instance != null)
        {
            //  return;
        }
        _instance = this;
    }
    public static ViligerTakeTool Instance
    {
        get
        {
            // if (_instance == null)
            // {
            new ViligerTakeTool();
            // }

            return _instance;
        }
    }
    public override void EnterState(AIViliger _owner)
    {
        
        //if (_owner.target1 != null && _owner.target1.GetComponent<Tool>().taked != true)
        //{
            //_owner.target1.GetComponent<Tool>().taked = true;
            _owner.StartCoroutine(_owner.TakeToolCo());
        //}

    }

    public override void ExitState(AIViliger _owner)
    {
       // Debug.Log("Exiting first state " + _owner.name);
    }

    public override void UpdateState(AIViliger _owner)
    {
        if (_owner.GetComponent<Tool>().Item!=null)
        {
            
                _owner.stateMachine.ChangeState(VilligerLookingForJobWithTool.Instance);
        }
        else
        {
            //_owner.stateMachine.ChangeState(VilligerLookingForTool.Instance);
        }
        
    }
}



