using System.Collections.Generic;
using UnityEngine;

public class ShootOptimalization : LogChaseOptimalization
{
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;
    public CircleCollider2D myCollider;
    public CircleCollider2D chaseColider;

    protected override void Start()
    {
        toAttack = new List<GameObject>();

        base.Start();
        myCollider.radius = 0.6f * chaseColider.radius;
    }

    protected override void Update()
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
        else
        {
            for (var i = toAttack.Count - 1; i > -1; i--)
            {
                if (toAttack[i] == null)
                    toAttack.RemoveAt(i);
            }
            if (toAttack.Count > 0)
            {
                target = toAttack[0].transform;
                if (log.currentState != EnemyState.stagger)
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
                else
                    canFire = false;
            }
        }
    }
}
