using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public Signal2 signal;
    public UnityEvent signalEvent;
    public void OnSignalRaised()
    {
        if (signal == null)
            Debug.LogError(this.name+ " Listener signal problem");
        signalEvent.Invoke();
    }
    private void OnEnable()
    {
        if (signal == null)
            Debug.LogError(this.name + " Listener signal problem");
        signal.RegisterListener(this);
    }
    private void OnDisable()
    {
        if (signal == null)
            Debug.LogError(this.name + " Listener signal problem");
        signal.DeRegisterListener(this);
    }
}
