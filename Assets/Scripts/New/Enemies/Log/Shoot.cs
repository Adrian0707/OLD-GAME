using UnityEngine;

public class Shoot : LogChase
{
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;

    // Start is called before the first frame update


    // Update is called once per frame
    private void Update()
    {
        if (canFire == false)
        {
            fireDelaySeconds -= Time.deltaTime;
            if (fireDelaySeconds <= 0)
            {
                canFire = true;
                fireDelaySeconds = fireDelay;
            }
        }

    }

/*    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }*/

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        if (collision.gameObject.CompareTag(viligersTag) || collision.gameObject.CompareTag(playerTag))
        {
            if (target != null && Vector2.Distance(target.position, transform.position) < gameObject.GetComponent<CircleCollider2D>().radius)
            {
                if (log.currentState == EnemyState.idle || log.currentState == EnemyState.walk && log.currentState != EnemyState.stagger)
                {
                    if (canFire)
                    {
                        Vector3 tempVector = target.transform.position - transform.position;
                        GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                        current.SetActive(true);
                        current.GetComponent<Projectile>().damage = 1;
                        current.GetComponent<Projectile>().Launch(tempVector);
                        canFire = false;
                        log.ChangeState(EnemyState.walk);
                        anim.SetBool("wakeUp", true);
                    }
                }
            }
            else if (log.currentState != EnemyState.stagger)
            {
               // myRigidbody.bodyType = RigidbodyType2D.Static;
                anim.SetBool("wakeUp", false);
                log.ChangeState(EnemyState.idle);
            }

        }
    }

}
