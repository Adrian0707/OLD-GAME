using UnityEngine;
[CreateAssetMenu]
[System.Serializable]
public class BoolValue : ScriptableObject //, ISerializationCallbackReceiver
{
    public bool initialValue;

    //[HideInInspector]
    public bool RuntimeValue;

/*    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }
    public void OnBeforeSerialize()
    {

    }*/

}
