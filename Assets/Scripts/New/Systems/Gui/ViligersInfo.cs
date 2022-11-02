using UnityEngine;
using TMPro;

public class ViligersInfo : MonoBehaviour
{
   // public GameObject viligerHolder;
    private Fireplace fireplace;
    // Start is called before the first frame update
    void Start()
    {
        fireplace = GameObject.FindGameObjectWithTag("Fireplace").GetComponent<Fireplace>();
       transform.Find("Text").GetComponent<TextMeshProUGUI>().text = 
            fireplace.fireplaceStats.maxViligers.Value.ToString()+"/"+ GameObject.FindGameObjectsWithTag("Viliger").Length;
    }

    public void Change()
    {
        if (transform.Find("Text") != null)
        {
            transform.Find("Text").GetComponent<TextMeshProUGUI>().text =
               fireplace.fireplaceStats.maxViligers.Value.ToString() + "/" + GameObject.FindGameObjectsWithTag("Viliger").Length;
        }
    }
}
