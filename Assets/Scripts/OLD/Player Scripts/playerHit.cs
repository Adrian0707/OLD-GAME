using UnityEngine;

public class playerHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("breakable"))
        {
            collision.GetComponent<pod>().Smash();
        }
    } 

}
