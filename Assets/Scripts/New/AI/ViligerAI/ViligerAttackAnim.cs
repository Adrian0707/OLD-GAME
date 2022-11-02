using StateStuff;
public class ViligerAttackAnim : State<AIViliger>
{
    private int times;
    private static ViligerAttackAnim _instance;
    private ViligerAttackAnim()

    {
        if (_instance != null)
        {
          //  return;
        }
        _instance = this;
        times = 0;
    }
    public static ViligerAttackAnim Instance
    {
        get
        {
           // if (_instance == null)
            //{
                new ViligerAttackAnim();
            //}

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
        // if (_owner.targetGoTo != null) 
        //   { 
       // LookAt(_owner.targetGoTo.transform, _owner);
        if (_owner.gameObject.GetComponent<Tool>().Item.name == "Axe")
        {
            ActionForAxe(_owner);
        }
        if (_owner.gameObject.GetComponent<Tool>().Item.name == "Hammer")
        {
            ActionForHammer(_owner);
        }
        if (_owner.gameObject.GetComponent<Tool>().Item.name == "Pick")
        {
            ActionForPick(_owner);
        }
       // }
       // else
       // {
            //_owner.StartCoroutine(_owner.LookingforTarget());
          //  _owner.stateMachine.ChangeState(ViligerAttackAnim.Instance);
      //  }

    }
    void ActionForPick(AIViliger _owner)
    {
        //gameobject destroyed 
        if (_owner.resourcesHeld <= _owner.viligerStatistics.capicity.Value && _owner.targetGoTo != null)
        {
            if (_owner.resourcesHeld == 0)
            {
                _owner.resourceName = _owner.targetGoTo.name;
                _owner.resorceImage = _owner.targetGoTo.GetComponent<Stone>().dropp;
            }
            if (!_owner.corutineIsRunning)
            {


                _owner.StartCoroutine(_owner.AttackCo());
                _owner.resourcesHeld++;
                _owner.targetGoTo.GetComponent<Stone>().Damage(1);
            }

        }
        else
        {
            if (_owner.resourcesHeld != 0)
            {
                // _owner.target1 = GameObject.FindGameObjectWithTag("breakable");
                _owner.stateMachine.ChangeState(ViligerTake.Instance);
            }
            else
            {
                _owner.stateMachine.ChangeState(VilligerLookingForJobWithTool.Instance);
            }
        }
    }
    void ActionForAxe(AIViliger _owner)
    {
        if (_owner.resourcesHeld <= _owner.viligerStatistics.capicity.Value && _owner.targetGoTo != null)
        {
            if (_owner.resourcesHeld == 0)
            {
                _owner.resourceName = _owner.targetGoTo.name;
                _owner.resorceImage = _owner.targetGoTo.GetComponent<Tree>().dropp;
            }
            if (!_owner.corutineIsRunning)
            {


                _owner.StartCoroutine(_owner.AttackCo());
                _owner.resourcesHeld++;
                _owner.targetGoTo.GetComponent<Tree>().Damage(.5f);
            }

        }
        else
        {
            if (_owner.resourcesHeld != 0)
            {
                // _owner.target1 = GameObject.FindGameObjectWithTag("breakable");
                _owner.stateMachine.ChangeState(ViligerTake.Instance);
            }
            else
            {
                if (!_owner.corutineIsRunning)
                {
                    _owner.StartCoroutine(_owner.LookingforTarget());
                    _owner.stateMachine.ChangeState(VilligerLookingForJobWithTool.Instance);
                }
            }
        }
    }
    void ActionForHammer(AIViliger _owner)
    {
        if (!_owner.targetGoTo.GetComponent<Building>().isConstructed)
        {
            if (!_owner.corutineIsRunning)
            {
                _owner.StartCoroutine(_owner.AttackCo());
                _owner.targetGoTo.GetComponent<Building>().Construct(1);
            }
        }
        else
        {
            _owner.stateMachine.ChangeState(VilligerLookingForJobWithTool.Instance);
        }
    }
   /* void LookAt(Transform at, AIViliger _owner)
    {
        Vector3 change = _owner.transform.position - at.position;
        change = change.normalized;

        _owner.Animator.SetFloat("moveX", change.x);
        _owner.Animator.SetFloat("moveY", change.y);
        _owner.Animator.SetBool("moving", false);
    }*/
}



