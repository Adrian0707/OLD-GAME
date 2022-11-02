using System.Collections;
using UnityEngine;

public class UpdatePathFinding : MonoBehaviour
{
    private AstarPath path;
    private bool updating;

    void Awake()
    {
        updating = false;
        path = GetComponent<AstarPath>();
       //path.ScanAsync();
        //InvokeRepeating("UpdatePath",0, 1f);
    }

    // Update is called once per frame

  public void UpdatePath()
    {
       
      //  Debug.LogError("Path upade");
        path.Scan();
        // path.ScanAsync();
       /* if (updating == false)
        {
            StartCoroutine(updateAsyncCo());
        }*/
    }
    IEnumerator updateAsyncCo()
    {
        updating = true;
        foreach (var a in path.ScanAsync(path.data.graphs))
        {
            yield return null;
        }
        updating = false;
    }

   
}
