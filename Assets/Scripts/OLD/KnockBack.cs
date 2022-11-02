using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class KnockBack : MonoBehaviour
{
    [SerializeField] private float thrust;
    [SerializeField] private float knockTime;
    public string[] collisionTags;

    IEnumerator FakeAddForceMotion(Rigidbody2D myRigidBody2D)
    {
        float i = 0.001f;
        while (thrust > i)
        {
            if (myRigidBody2D != null)
            {
                myRigidBody2D.velocity = new Vector2(thrust * 2 / i, myRigidBody2D.velocity.y); // !! For X axis positive force
            }
            i = i + Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }
        myRigidBody2D.velocity = Vector2.zero;
        yield return null;
    }
    void AddForce(Rigidbody2D myRigidBody2D)
    {
        StartCoroutine(FakeAddForceMotion(myRigidBody2D));
    }
    public void OnTriggerEnter2D(Collider2D other)
    {

        foreach (string collisionTag in collisionTags)
        {


            if (other.gameObject.CompareTag(collisionTag) && other.isTrigger)
            {

                Rigidbody2D hit = other.GetComponentInParent<Rigidbody2D>();
                if (hit != null)
                {
                  //  print("IsRigid");
                    Vector3 difference = (hit.transform.position - transform.position) * Time.deltaTime;
                    // Debug.DrawLine(hit.transform.position, transform.position, Color.red);
                    difference = difference.normalized * thrust;
                    //hit.MovePosition(difference);
                    // hit.DOMove(hit.transform.position + difference, knockTime);
                    if (hit.bodyType == RigidbodyType2D.Dynamic)
                    {
                        hit.AddForce(difference, ForceMode2D.Impulse);
                        //StartCoroutine(FlyEffectCo(knockTime, other));
                    }
                    else
                    {
                        hit.bodyType = RigidbodyType2D.Dynamic;
                        hit.AddForce(difference, ForceMode2D.Impulse);
                    }


                    if (other.gameObject.CompareTag("EnemyHealth") && other.isTrigger)
                    {
                        hit.GetComponentInParent<Enemy>().Knock(hit, knockTime);
                    }
                    else if (other.gameObject.CompareTag("PlayerHealth"))
                    {
                        if (other.GetComponentInParent<Movement>() != null && other.GetComponentInParent<Movement>().currentState != PlayerState.stagger)
                        {

                            hit.GetComponentInParent<Movement>().Knock(knockTime);

                        }
                    }

                }
            }
        }
    }

    IEnumerator FlyEffectCo(float time, Collider2D collider)
    {
        if (collider != null)
        {
            collider.gameObject.SetActive(false);
            Vector3 orgScale2 = collider.gameObject.transform.parent.localScale;
            Vector3 orgScale = collider.gameObject.transform.parent.localScale;
            for (int i = 0; i <= 10; i++)
            {
                if (collider != null)
                    collider.gameObject.transform.parent.localScale = orgScale * 0.02f * i + orgScale;
                yield return new WaitForSeconds(time / 20f);
            }
            if (collider != null)
                orgScale = collider.gameObject.transform.parent.localScale;
            for (int i = 0; i <= 10; i++)
            {
                if (collider != null)
                    collider.gameObject.transform.parent.localScale = -orgScale * 0.02f * i + orgScale;
                yield return new WaitForSeconds(time / 20f);
            }
            collider.gameObject.transform.parent.localScale = orgScale2;
            collider.gameObject.SetActive(true);
        }
    }
}
