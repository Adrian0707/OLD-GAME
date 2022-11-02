using System.Collections;
using Pathfinding;
using UnityEngine;

public enum PlayerState1
{
    walk,
    attack,
    interact,
    stagger,
    idle,
    ability
}

public class Movment1 : MonoBehaviour
{   public AIPath aIPath;
    public Joystick joystick;
    public PlayerState currentState;
    public float speed;
    
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    //Todo break off the health system to new component 
    /*public FloatValue currentHealth;
    public Signal2 playerHealthSignal;*/

    public ventorValue startingPosition;
    //Todo break off the player inventory to new component :(
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;
    //todo should be part of the health system?
    public Signal2 playerHit;
    //Todo should by part of magic system
    public Signal2 decreaseMagic;
    //todo break this of with the player ability system
    [Header("Projectile tuff")]
    public GameObject projectile;
    public Item bow;
    //todo break of should be in own script
    [Header("IFrame Stuff")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerColider;
    public SpriteRenderer mySprite;
    [SerializeField] private GenericAbility currentAbility;
    private Vector2 tempMovment = Vector2.down;
    private Vector2 facingDirection = Vector2.down;
        

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        //is the player in an interaction
        if(currentState== PlayerState.interact)
        {
            return;
        }
        change = Vector3.zero;
        change.x = aIPath.velocity.x; //Input.GetAxisRaw("Horizontal");
        change.y = aIPath.velocity.y; //Input.GetAxisRaw("Vertical");
        //Debug.Log(change);
        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack
            && currentState!= PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (Input.GetButtonDown("second weapon") && currentState != PlayerState.attack
            && currentState != PlayerState.stagger)
        {
            if (playerInventory.CheckForItem(bow))
            {
                StartCoroutine(SecondAttackCo()); 
            }
        }
        else if (Input.GetButtonDown("Ability"))
        {
            if (currentAbility)
            {
                StartCoroutine(AbilityCo(currentAbility.duration));
            }
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpadeAnimationAndMove();
        }

    }
    private IEnumerator AttackCo ()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        if(currentState!= PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
        
    }
    private IEnumerator SecondAttackCo()
    {
        //animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        MakeArrow();
        //animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }

    }
    //should be part of ability system
    private void MakeArrow()
    {
        if (playerInventory.currentMagic >0 )
        {
            Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
            Arrow arrow = Instantiate(projectile, transform.transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.Setup(temp, ChooseArrowDirection());
            playerInventory.ReduceMagic(arrow.magicCost);
            decreaseMagic.Raise();
        }
    }
    //part of ability 
    Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }
    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                animator.SetBool("receiveItem", true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                animator.SetBool("receiveItem", false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }
    void UpadeAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            animator.SetFloat("moveX", change.x); 
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
            facingDirection = change;
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }
    void MoveCharacter()
    {
        change.Normalize();
        if(change.x * change.y == 0)
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
        else
            myRigidbody.MovePosition(transform.position + change * speed/(float)1.4 * Time.deltaTime);
    }
    //todo own script 
    public void Knock(float knockTime)
    {
        StartCoroutine(KnockCo(knockTime));
        /*currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0)
        {
           
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }*/
    }
    private IEnumerator KnockCo(float knockTime)
    {
        playerHit.Raise();
        if (myRigidbody != null)
        {
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
    private IEnumerator FlashCo()
    {
        int temp = 0;
        triggerColider.enabled = false;
        while (temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerColider.enabled = true;
    }
    public IEnumerator AbilityCo(float abilityDuration)
    {
        currentState = PlayerState.ability;
        currentAbility.Ability(transform.position, facingDirection, animator, myRigidbody);
         yield return new WaitForSeconds(abilityDuration);
        currentState = PlayerState.idle;

    }
}
