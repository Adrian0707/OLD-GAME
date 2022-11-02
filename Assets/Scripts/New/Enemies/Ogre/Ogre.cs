public class Ogre : Enemy
{
   public GenericDamage[] genericDamages;
   public OgreChase ogreChase;
   public GenericHealth genericHealth;
    public override void Start()
    {
        base.Start();
        mySprite.color = enemyStatistics.color;
        foreach (GenericDamage item in genericDamages)
        {
            item.statistics = this.enemyStatistics;
        }
        ogreChase.enemyStatistics = this.enemyStatistics;
        genericHealth.statistics = this.enemyStatistics;
    }
}
