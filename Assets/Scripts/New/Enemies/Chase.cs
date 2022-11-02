using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class Chase : MonoBehaviour
{
    public string viligersTag = "Viliger";
    public string playerTag = "Player";
    protected Transform target;
    public EnemyStatistics enemyStatistics;
    protected Rigidbody2D myRigidbody;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        myRigidbody = gameObject.transform.parent.GetComponent<Rigidbody2D>();
       
       
    }


   
 }
    



