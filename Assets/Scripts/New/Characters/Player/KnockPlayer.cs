using System.Collections;
using UnityEngine;

public class KnockPlayer : MonoBehaviour
{


    public Movement movment;
    private Rigidbody2D myRigidbody;
   // public Signal2 playerHit;
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerColider;
    public SpriteRenderer mySprite;



    public void Knock(float knockTime)
    {
        StartCoroutine(KnockCo(knockTime));
        /*currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0)
        {
           
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }*/
    }
    private IEnumerator KnockCo(float knockTime)
    {
        // playerHit.Raise();
        if (myRigidbody != null)
        {
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            movment.currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
    private IEnumerator FlashCo()
    {
        int temp = 0;
        triggerColider.enabled = false;
        while (temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerColider.enabled = true;
    }

}
