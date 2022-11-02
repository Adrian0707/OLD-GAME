
using UnityEngine;
using StateStuff;
using Pathfinding;
public class ViligerNight : State<AIViliger>
{



    private Vector3 change;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;


    private static ViligerNight _instance;
    private ViligerNight()

    {
        if (_instance != null)
        {
            //  return;
        }
        _instance = this;
    }
    public static ViligerNight Instance
    {
        get
        {
            // if (_instance == null)
            // {
            new ViligerNight();
            // }

            return _instance;
        }
    }
    public override void EnterState(AIViliger _owner)
    {
       
    }

    public override void ExitState(AIViliger _owner)
    {
       
    }

    public override void UpdateState(AIViliger _owner) 
    {

     

       
    }

  
}



