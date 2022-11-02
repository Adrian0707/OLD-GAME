namespace StateStuff
{
    public class StateMachine <T>
    {
        public State<T> curentState { get; private set; }
        public T Owner;
        public StateMachine (T _o)
        {
            Owner = _o;
            curentState = null;
        }
        public void ChangeState(State<T> _newstate)
        {
            if (curentState != null)
            {
                curentState.ExitState(Owner);
            }
            curentState = _newstate;
            curentState.EnterState(Owner);
        }
        public void Update()
        {
            if (curentState != null)
            {
                curentState.UpdateState(Owner);
            }
        }
    }
    public abstract class State<T>
    {
        public abstract void EnterState(T _owner);
        public abstract void ExitState(T _owner);
        public abstract void UpdateState(T _owner);
    }
}