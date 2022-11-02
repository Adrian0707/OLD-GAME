using UnityEngine.Rendering.Universal;
public class GhoustBasic : Enemy
{
    public Light2D light2D;
    public GhoustDamage ghoustDamage;
    public GenericHealth genericHealth;
    public GhoustChase ghoustChase;

    public override void Start()
    {
        base.Start();
        light2D = GetComponent<Light2D>();
        light2D.color = enemyStatistics.color;
     
    }
    private void Awake()
    {
        ghoustChase.enemyStatistics = this.enemyStatistics;
        genericHealth.statistics = this.enemyStatistics;
        ghoustDamage.statistics = this.enemyStatistics;
    }
}
