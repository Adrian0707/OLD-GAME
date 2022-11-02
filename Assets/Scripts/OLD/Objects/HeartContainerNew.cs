using UnityEngine;

public class HeartContainerNew : PowerUp
{
    public FloatValue heartContainers;
    public FloatValue playerHealth;

    public HeartContainerNew(string type, int price) : base(type, price)
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&& playerHealth.RuntimeValue <= 10)
        {
            heartContainers.RuntimeValue += 0.5f;
            playerHealth.RuntimeValue = heartContainers.RuntimeValue * 2;
            //powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
