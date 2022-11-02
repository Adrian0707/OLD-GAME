using UnityEngine;

public class GameOver : MonoBehaviour
{
    private GenericHealth health;
    public Signal2 signalGameOver;
    public GameObject destroyEffect;
    void Start()
    {
        health = gameObject.GetComponent<GenericHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.currentHealth <= 0)
        {
            signalGameOver.Raise();
            if (destroyEffect != null)
            {
                GameObject eff = Instantiate(destroyEffect);
                eff.transform.position = transform.position;
            }
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("ViligerHealth"))
            {
                item.GetComponent<ViligerHealth>().InstantDeath();
            }
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
