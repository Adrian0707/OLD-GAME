using System.Collections;
using UnityEngine;

public class GameRunning : MonoBehaviour
{
    public GameObject player;
    public GameObject deadEffect;
    public GameObject respownEffect;
    public Signal2 gameOverSignal;
    public void PlayerDead()
    {
        StartCoroutine(PlayerRespawn());
    }
    public void GameOver()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("ViligerHealth"))
        {
            item.GetComponent<ViligerHealth>().InstantDeath();
        }
    }
    private IEnumerator PlayerRespawn()
    {
        SpriteRenderer mySprite = player.GetComponent<SpriteRenderer>();
        Collider2D hitbox = player.transform.Find("PlayerHealth").GetComponent<Collider2D>();
        // hitbox.enabled = false;
        /* while (player.GetComponent<Movment>().currentState == PlayerState.stagger)
         {
             yield return new WaitForEndOfFrame();
         }*/
        player.GetComponent<Movement>().currentState = PlayerState.idle;
        hitbox.enabled = false;
        if (deadEffect != null)
        {
            GameObject effect = GameObject.Instantiate(deadEffect, player.transform.position + new Vector3(0, 2.4f, 0), Quaternion.identity);
           // effect.transform.localScale = new Vector3(3, 3, 0);
          //  effect.GetComponent<SpriteRenderer>().color = Color.blue;
            effect.GetComponent<SpriteRenderer>().sortingOrder = mySprite.sortingOrder+1;
            Destroy(effect, 1f);
           
        }
        yield return new WaitForSeconds(0.2f);
        //player.SetActive(false);

        //yield return new WaitForSeconds(10f);
        if (player != null && GameObject.FindGameObjectWithTag("Fireplace") != null)
        {
            player.transform.position = GameObject.FindGameObjectWithTag("Fireplace").transform.position;
            GameObject effect = GameObject.Instantiate(respownEffect, player.transform.position+ new Vector3(0, 2, 0), Quaternion.identity);
           // effect.transform.localScale = new Vector3(3, 3, 0);
           // effect.GetComponent<SpriteRenderer>().color = Color.blue;
            effect.GetComponent<SpriteRenderer>().sortingOrder = mySprite.sortingOrder+2;
            Destroy(effect, 1f);
            yield return new WaitForSeconds(0.2f);
            if (GameObject.FindGameObjectWithTag("GUI").GetComponent<Gui>().gold>=10)
            {
                GameObject.FindGameObjectWithTag("GUI").GetComponent<Gui>().ModifyGold(-10);
                player.SetActive(true);
            }
            else
            {
                gameOverSignal.Raise();
            }
            hitbox.enabled = true;
        }
    }
   // private IEnumerator
}
