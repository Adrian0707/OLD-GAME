using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Signal2 : ScriptableObject
{
    public IList<SignalListener> listeneners = new List<SignalListener>();
    public void Raise()
    {
        for (int i = listeneners.Count - 1; i >= 0; i--)
        {
            listeneners[i].OnSignalRaised();
        }
    }
    public void RegisterListener(SignalListener listener)
    {
        listeneners.Add(listener);
    }
    public void DeRegisterListener(SignalListener listener)
    {
        listeneners.Remove(listener);
    }
}
