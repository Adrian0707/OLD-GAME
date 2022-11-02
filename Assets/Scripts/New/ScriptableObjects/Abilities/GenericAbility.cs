
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Abilities/GenericAbility",fileName ="New Generic Ability")]
public class GenericAbility : ScriptableObject
{
   // public float magicCost;
    public float duration;

   // public FloatValue playerMagic;
    public Signal2 usePlayerMagic;
 
    public virtual void Ability(Vector2 playerPosition, Vector2 playerFacingDrection , Animator playerAnimator=null,
        Rigidbody2D playerRigidbody = null) 
    {

    }
}
