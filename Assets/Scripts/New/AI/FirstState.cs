
using UnityEngine;
using StateStuff;

public class FirstState : State<AI>
{
    private static FirstState _instance;
    private FirstState()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }
    public static FirstState Instance
    {
        get
        {
            if (_instance == null)
            {
                new FirstState();
            }

            return _instance;
        }
    }
    public override void EnterState(AI _owner)
    {
        Debug.Log("Enter first state");
    }

    public override void ExitState(AI _owner)
    {
        Debug.Log("Exiting first state");
    }

    public override void UpdateState(AI _owner)
    {
        //Debug.Log("first state");
        if (_owner.switchState)
        {
            _owner.stateMachine.ChangeState(SecondState.Instance);
        }
    }
}
