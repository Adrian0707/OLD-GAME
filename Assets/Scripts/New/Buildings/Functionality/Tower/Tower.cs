using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerStats towerStats;
    public GameObject projectile;
   // public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;
    public string tagEnemy = "Enemies";

    private void Update()
    {
        if (canFire == false)
        {
            fireDelaySeconds -= Time.deltaTime;
            if (fireDelaySeconds <= 0)
            {
                canFire = true;
                fireDelaySeconds = towerStats.FierSpeed.Value;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
       // print(collision.name);
        if (collision.CompareTag(tagEnemy))
        { 
        
        if (canFire)
        {
            Vector3 tempVector = collision.transform.position - transform.position;
/*            projectile.GetComponent<Projectile>().moveSpeed = towerStats.ProjectileSpeed.Value;
            projectile.GetComponent<Projectile>().lifeTime = towerStats.ProjectileLifetime.Value;
            projectile.GetComponent<Projectile>().radSize= towerStats.ProjectileSize.Value;
            projectile.GetComponent<Projectile>().damage = towerStats.ProjectileDamage.Value;
            projectile.GetComponent<Projectile>().durability = towerStats.ProjectileDurability.Value;*/
            projectile.GetComponent<Projectile>().ProjectileSetStats(
                towerStats.ProjectileSpeed.Value,
                towerStats.ProjectileDamage.Value,
                towerStats.ProjectileLifetime.Value,
                towerStats.ProjectileDurability.Value,
                towerStats.ProjectileSize.Value
                );
            GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
            current.SetActive(true);
            current.GetComponent<Projectile>().Launch(tempVector.normalized*10);
            canFire = false;
        }
        }
    }
    public void setRadius()
    {
        gameObject.transform.Find("Active").GetComponent<CircleCollider2D>().radius = towerStats.Rad.Value;
    }

}