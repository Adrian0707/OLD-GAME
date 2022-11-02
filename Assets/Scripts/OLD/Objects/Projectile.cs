using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movment Stuff")]
    public float moveSpeed = 1.5f;
    public Vector2 directiontoMove;
    [Header("Collison Stuff")]
    [SerializeField] private string[] collisionStrings;
    public float damage;
    [Header("Lifetime")]
    public float lifeTime= 3;
    private float lifeTimeSeconds;
    public float durability=0;
    [Header("Object")]
    public Rigidbody2D myRigidbody;
    public float radSize=0.25f;
    


    // Start is called before the first frame update
    void Start()
    {
        lifeTimeSeconds = lifeTime;
        myRigidbody = GetComponent<Rigidbody2D>();
        GetComponent<CircleCollider2D>().radius = radSize;
        GetComponent<GenericDamage>().damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimeSeconds -= Time.deltaTime;
        if (lifeTimeSeconds <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void Launch(Vector2 initialVel)
    {
        myRigidbody.velocity = initialVel.normalized * moveSpeed;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
        {
            foreach (string collisionString in collisionStrings)
            {
                if (collision.tag == collisionString)
                {
                    
                        durability--;
                        if (durability <= 0)
                            Destroy(this.gameObject);
                    

                    }
            }
        }
    }

    public void ProjectileSetStats(float moveSpeed, float damage, float lifeTime, float durability, float rad)
    {
        this.moveSpeed = moveSpeed;
        this.damage = damage;
        this.lifeTime = lifeTime;
        this.durability = durability;
        this.radSize = rad;
    }
}
