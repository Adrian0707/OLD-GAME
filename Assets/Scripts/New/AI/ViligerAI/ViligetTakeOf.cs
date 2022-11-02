
using UnityEngine;
using StateStuff;
using Pathfinding;
public class ViligerTakeOf : State<AIViliger>
{



    private Vector3 change;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    
    private static ViligerTakeOf _instance;
    private ViligerTakeOf()

    {
        if (_instance != null)
        {
            //  return;
        }
        _instance = this;
    }
    public static ViligerTakeOf Instance
    {
        get
        {
            // if (_instance == null)
            // {
            new ViligerTakeOf();
            // }

            return _instance;
        }
    }
    public override void EnterState(AIViliger _owner)
    {
       // Debug.Log("Take of");
        _owner.StartCoroutine(_owner.TakeCoOf());
        
    }

    public override void ExitState(AIViliger _owner)
    {
       // Debug.Log("Exiting TakeOf state " + _owner.name);
    }

    public override void UpdateState(AIViliger _owner)
    {
        if (!_owner.corutineIsRunning)
        {
            _owner.stateMachine.ChangeState(VilligerLookingForJobWithTool.Instance);
           /* if (GameObject.FindGameObjectsWithTag("tree").Length != 0)
            {
                GameObject nearest = null;
                foreach (GameObject gObj in GameObject.FindGameObjectsWithTag("tree"))
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
                _owner.target1 = nearest;
                _owner.stateChain.Push(ViligerAttackAnim.Instance);
                _owner.stateMachine.ChangeState(ViligerGo.Instance);*/
                //_owner.stateMachine.ChangeState(ViligerGo.Instance);
            }
        }
    }




