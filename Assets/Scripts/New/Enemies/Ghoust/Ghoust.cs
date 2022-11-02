using UnityEngine;

public class Ghoust : MonoBehaviour
{
  
    public int moveSpeed = 2;
    Transform target;
    Rigidbody2D rigidbody2D;
    public string viligersTag;
    public string playerTag;
    public string buildingsTag;
    bool Attack = false;
    private GenericHealth healt;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("house").transform;
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
       healt= gameObject.transform.Find("Health").GetComponent<GenericHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healt.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        if (target != null)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            // ChangeAnim(temp - transform.position);
            rigidbody2D.MovePosition(temp);
        }
   
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(viligersTag)|| collision.gameObject.CompareTag(playerTag)|| collision.gameObject.CompareTag(buildingsTag))
        {
            target = collision.transform;
        }
    }
    /*private void OnTriggerStay2D(Collider2D collision)
    {
        
           if ((collision.gameObject.CompareTag(viligersTag) || collision.gameObject.CompareTag(playerTag) || collision.gameObject.CompareTag(buildingsTag)))
            {
                target = collision.transform;
            }*/
            /*else
            {
                target = GameObject.FindGameObjectWithTag("house").transform;
            }

        
    }*/
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(viligersTag) || collision.gameObject.CompareTag(playerTag) || collision.gameObject.CompareTag(buildingsTag))
        {
            target = GameObject.FindGameObjectWithTag("house").transform;
        }
    }
}
