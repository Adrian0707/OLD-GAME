using UnityEngine;

public class Heart : PowerUp
{
    public PlayerHealth playerHealth;

    public Heart(string type, int price) : base(type, price)
    {
    }



    // public FloatValue heartContaners;
    // Start is called before the first frame update
    void Start()
    {
        name = "Heart";
        //  playerHealth=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            if (playerHealth == null)
            {
                playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerHealth>();
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerHealth")&& collision.isTrigger)
        {
            if (playerHealth)
            {
                playerHealth.Heal(1);
            }
            //powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
