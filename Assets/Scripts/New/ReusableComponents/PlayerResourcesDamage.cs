using UnityEngine;
public enum Resorce
{
    Stone,
    Wood,
    Gold,
    Coin
}
public class PlayerResourcesDamage : GenericDamage
{
    [SerializeField] protected Gui gui;
    //public Resorce res;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string collisionTag in collisionTags)
        {


            if (collision.gameObject.CompareTag(collisionTag) && collision.isTrigger)
            {
                GenericHealth temp = collision.GetComponent<GenericHealth>();
                if (temp)
                {
                    //print(temp.currentHealth);
                   
                    switch (collisionTag)
                    {
                        case "Stone":
                            if (statistics == null || !(statistics is PlayerStatistics))
                            {

                                temp.Damage(damage);
                                gui.ModifyStone(1);
                                //print(damage);
                            }
                            else
                            {
                                PlayerStatistics player = (PlayerStatistics)statistics;
                                temp.Damage(player.stoneDamage.Value);
                                gui.ModifyStone((int)player.stoneGetting.Value);
                              //  print(player.stoneDamage.Value);
                            }
                            
                            break;
                        case "Tree":
                            if (statistics == null || !(statistics is PlayerStatistics))
                            {

                                temp.Damage(damage);
                                gui.ModifyWood(1);
                                //print(damage);
                            }
                            else
                            {
                                PlayerStatistics player = (PlayerStatistics)statistics;
                                temp.Damage(player.woodDamage.Value);
                                gui.ModifyWood((int)player.woodGetting.Value);
                               // print(player.woodDamage.Value);
                            }
                           
                            break;
                        default:
                            break;
                    }
                }


            }
        }
    }
       
    
    

}
