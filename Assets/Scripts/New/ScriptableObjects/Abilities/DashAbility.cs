
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "ScriptableObject/Abilities/DashAbility", fileName = "Dash Ability")]
public class DashAbility : GenericAbility
{
    public float dashForce;

    public override void Ability(Vector2 playerPosition, Vector2 playerFacingDrection, Animator playerAnimator = null, Rigidbody2D playerRigidbody = null)
    {

        /*if (playerMagic.RuntimeValue >= magicCost)
        {
            playerMagic.RuntimeValue -= magicCost;
            usePlayerMagic.Raise();
        }
        else
        {
            return;
        }*/
        if (playerRigidbody)
        {
            Vector3 dashVector = playerRigidbody.transform.position + (Vector3)playerFacingDrection.normalized*dashForce;
            playerRigidbody.DOMove(dashVector, duration);
        }
    }
}
