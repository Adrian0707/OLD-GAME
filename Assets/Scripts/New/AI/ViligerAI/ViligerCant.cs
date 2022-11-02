
using UnityEngine;
using StateStuff;
using Pathfinding;
public class ViligerCant : State<AIViliger>
{

   
    
    private Vector3 change;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    private float nextUpdate = 1f;
    Seeker seeker;
    Rigidbody2D rb;

    
    private static ViligerCant _instance;
    private ViligerCant()

    {
        if (_instance != null)
        {
          //  return;
        }
        _instance = this;
    }
    public static ViligerCant Instance
    {
        get
        {
           // if (_instance == null)
           // {
                new ViligerCant();
           // }

            return _instance;
        }
    }
    public override void EnterState(AIViliger _owner)
    {
        Debug.LogError("Viliger cant state");
        _owner.StartCoroutine(_owner.CantReachTarget());
        _owner.stateMachine.ChangeState(ViligerGo.Instance);
    }

    public override void ExitState(AIViliger _owner)
    {
      
    }

    public override void UpdateState(AIViliger _owner)
    {  
       
    }
}



