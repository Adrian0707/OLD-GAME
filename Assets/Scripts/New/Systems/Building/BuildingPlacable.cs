using System.Collections.Generic;
using UnityEngine;


public class BuildingPlacable : MonoBehaviour
{
    [HideInInspector]public List<Collider2D> collider2s = new List<Collider2D>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            collider2s.Add(collision);
          //  Debug.LogError(collision.name);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Object") )
        {
            collider2s.Remove(collision);
        }
    }
}
