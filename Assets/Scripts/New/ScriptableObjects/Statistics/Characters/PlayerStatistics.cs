using UnityEngine;
[CreateAssetMenu(menuName = "Stats/Characters/PlayerStatistics")]
public class PlayerStatistics : MovingObjectStatistics, Atacking
{
   // public Stat speed;
    public Stat attack;
    public Stat stoneGetting;
    public Stat stoneDamage;
    public Stat woodGetting;
    public Stat woodDamage;
    public Stat mana;

    public int GetAttack()
    {
        return (int)attack.Value;
    }
    /* protected override void Awake()
{
    base.Awake();
    speed = new Stat(10);
    attack = new Stat(1);
}*/
}
