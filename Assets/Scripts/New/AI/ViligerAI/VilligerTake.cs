
using UnityEngine;
using StateStuff;
using Pathfinding;
public class ViligerTake : State<AIViliger>
{



    private Vector3 change;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;


    private static ViligerTake _instance;
    private ViligerTake()

    {
        if (_instance != null)
        {
            //  return;
        }
        _instance = this;
    }
    public static ViligerTake Instance
    {
        get
        {
            // if (_instance == null)
            // {
            new ViligerTake();
            // }

            return _instance;
        }
    }
    public override void EnterState(AIViliger _owner)
    {//&& _owner.target1.GetComponent<Tree>().taked!=true
     /*if (_owner.target1.tag != "stump")
     {*/
     //_owner.target1.GetComponent<Tree>().taked = true;
        _owner.targetGoTo = null;
            _owner.StartCoroutine(_owner.TakeCo());
        //}
        /*else
        {
            _owner.stateMachine.ChangeState(VilligerLookingForJobWithTool.Instance);
        }*/

    }

    public override void ExitState(AIViliger _owner)
    {
        //Debug.Log("Exiting first state " + _owner.name);
    }

    public override void UpdateState(AIViliger _owner)
    {

        if (!_owner.corutineIsRunning)
        {
           /* if (_owner.target1 != null && _owner.target1.GetComponent<Tree>().taked != true)
            {

                //_owner.transform.FindChild("Received Item").GetComponent<SpriteRenderer>().sprite = _owner.target1.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;


               // _owner.DestroyOb(_owner.target1);
            }*/
            if (_owner.Animator.GetBool("carry"))
            {
                foreach (GameObject item in GameObject.FindGameObjectsWithTag("Storage"))
                {
                    if (item.GetComponent<Building>().isConstructed)
                    {
                        _owner.targetGoTo = item;
                //_owner.stateChain.Clear();//Coś pushuje wcześniej ale nie zbiera tego i zaburzenie jest jakies lookin for job
                    }
                    if (_owner.targetGoTo != null)
                    {
                        _owner.stateChain.Push(ViligerTakeOf.Instance);
                        _owner.stateMachine.ChangeState(ViligerGo.Instance);
                    }
                }
               
                
                
            }
            else
            {
                _owner.stateMachine.ChangeState(VilligerLookingForJobWithTool.Instance);
              /*  _owner.target1 = GameObject.FindGameObjectWithTag("tree");
                if (_owner.target1 != null)
                {
                    _owner.stateChain.Push(ViligerAttackAnim.Instance);
                    _owner.stateMachine.ChangeState(ViligerGo.Instance);
                }
                else
                {
                    _owner.stateMachine.ChangeState(VilligerIdle.Instance);
                }*/
            }
        }
    }
}



