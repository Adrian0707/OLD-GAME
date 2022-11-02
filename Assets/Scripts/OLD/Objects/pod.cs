using System.Collections;
using UnityEngine;

public class pod : MonoBehaviour
{
    private Animator anim;
    public LootTable thisLoot;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Smash()
    {
        anim.SetBool("smash", true);
        if (thisLoot != null)
        {
            PowerUp current = thisLoot.LootPowerUP().thisLoot;
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
        StartCoroutine(breakCo());
    }
    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);
    }
}
