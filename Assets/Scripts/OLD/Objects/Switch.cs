using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool active;
    public BoolValue storedValue;
    public Sprite activeSprite;
    private SpriteRenderer mySprite;
    public Door thisDoor;
    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        active = storedValue.initialValue;
        if (active)
        {
            ActivateSwitch();
        }
        
    }
    public void ActivateSwitch()
    {
        active = true;
        storedValue.RuntimeValue = active;
        thisDoor.Open();
        mySprite.sprite = activeSprite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //is it player
        if (collision.CompareTag("Player"))
        {
            ActivateSwitch();
        }
    }
}
