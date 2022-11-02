using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRigidbody;
    public float lifeTime;
    private float lifeTimeCounter;
    public float magicCost;
    // Start is called before the first frame update
    void Start()
    {
        lifeTimeCounter = lifeTime;
    }
    private void Update()
    {
        lifeTimeCounter -= Time.deltaTime;
        if (lifeTimeCounter <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void Setup(Vector2 velocity, Vector3 direction)
    {
        myRigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemies"))
        {
            Destroy(this.gameObject);
        }
    }
}
