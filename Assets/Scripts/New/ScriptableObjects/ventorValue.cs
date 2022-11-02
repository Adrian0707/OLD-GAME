using UnityEngine;
[CreateAssetMenu]
public class ventorValue : ScriptableObject, ISerializationCallbackReceiver
{
    [Header("Value running in game")]
    public Vector2 initialValue;
    [Header("Value by default when startineg")]
    public Vector2 defaultValue;

    public void OnAfterDeserialize()
    {
        initialValue = defaultValue;
    }

    public void OnBeforeSerialize()
    {
        
    }
}
