using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class FloatValue : ScriptableObject //, ISerializationCallbackReceiver
{
    public float initialValue;

    //[HideInInspector]
    public float RuntimeValue;
   
    private void OnEnable()
    {
        this.RuntimeValue = this.initialValue;
    }
    /*   public void OnAfterDeserialize()
       {
           RuntimeValue = initialValue;
       }
       public void OnBeforeSerialize()
       {

       }*/

}
