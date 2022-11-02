using UnityEngine;

public class ColectingItem : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PowerUp>())
        {

            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.y + 2;
            // print(i);
            Vector3 random = Random.insideUnitCircle.normalized;
            Vector3 toPlayer = this.transform.position - collision.transform.position;

            //Vector3 temp = Vector3.MoveTowards(transform.position, transform.position + random, 10 * Time.deltaTime * Random.Range(1, Mathf.Sqrt(current.amount)));
            //gameObject.GetComponent<Rigidbody2D>().MovePosition(temp);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(toPlayer.normalized *6, ForceMode2D.Force);
        }
    }
   
   
}
