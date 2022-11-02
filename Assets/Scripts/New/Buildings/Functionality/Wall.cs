using UnityEngine;
using UnityEngine.Rendering.Universal;
public class Wall : MonoBehaviour
{
    Light2D lightInZone;
    Light2D spriteInZone;
    GenericHealth health;
    public GameObject spriteZoneObject;
    public GameObject lightInZoneObject;
    public WallStats wallStats;
    public Building building;
    // Start is called before the first frame update
    void Start()
    {
        health = spriteZoneObject.GetComponent<GenericHealth>();
        spriteInZone = spriteZoneObject.GetComponent<Light2D>();
        lightInZone = lightInZoneObject.GetComponent<Light2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float mixColors= health.currentHealth / wallStats.health.Value ;
         
            spriteInZone.color = (1-mixColors)*Color.red+mixColors*Color.green;
        lightInZone.color = spriteInZone.color;



        if (health.currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void SetSize()
    {
        spriteZoneObject.GetComponent<CircleCollider2D>().radius = wallStats.Rad.Value;
        lightInZone.pointLightInnerRadius = (wallStats.Rad.Value)-1;
        lightInZone.pointLightOuterRadius = (wallStats.Rad.Value);
        spriteZoneObject.transform.localScale = new Vector3(
            (1.35f / wallStats.Rad.BaseValue) * wallStats.Rad.Value,
            (1.65f / wallStats.Rad.BaseValue) * wallStats.Rad.Value,
            0);



    }
    public void SetHealth()
    {
        health.FullHeal();
    }

    public void NightMode()
    {
        if(building.isConstructed)
        spriteZoneObject.SetActive(true);
    }
    public void DayMode()
    {
        spriteZoneObject.SetActive(false);
    }

}
