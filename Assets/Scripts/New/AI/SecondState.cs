
using UnityEngine;
using StateStuff;

public class SecondState : State<AI>
{
    private static SecondState _instance;
    private SecondState()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }
    public static SecondState Instance
    {
        get
        {
            if (_instance == null)
            {
                new SecondState();
            }

            return _instance;
        }
    }
    public override void EnterState(AI _owner)
    {
        Debug.Log("Enter SecondState state");
    }

    public override void ExitState(AI _owner)
    {
        Debug.Log("Exiting SecondState state");
    }

    public override void UpdateState(AI _owner)
    {
        //Debug.Log("SecondState state");
        if (!_owner.switchState)
        {
            _owner.stateMachine.ChangeState(FirstState.Instance);
        }
    }
}
