using UnityEngine;

public class Tool : MonoBehaviour
{
    public InventoryItem Item;
    public volatile bool taked = false;
    //Stack<GameObject> freeViligers;
    void Start()
    {
       // gameObject.GetComponent<SpriteRenderer>().sprite = Item.itemImage;
    }
    // Update is called once per frame
    void Update()
    {

       /* if (GameObject.FindGameObjectWithTag("house").GetComponent<Fireplace>().viligers.Count > 0  )
        {
           // freeViligers = new Stack<GameObject>(GameObject.FindGameObjectWithTag("house").GetComponent<Fireplace>().viligers);
            
            Debug.LogError("Stack is not empty");
            if (taked) { return; }

            else 
            {
                taked = true;
                GameObject a = GameObject.FindGameObjectWithTag("house").GetComponent<Fireplace>().viligers.First.Value;
                GameObject.FindGameObjectWithTag("house").GetComponent<Fireplace>().viligers.RemoveFirst();
                Debug.Log(a.name);
                a.GetComponent<AIViliger>().target1 = this.gameObject;
                a.GetComponent<AIViliger>().stateChain.Push(ViligerTakeTool.Instance);
                a.GetComponent<AIViliger>().stateMachine.ChangeState(ViligerGo.Instance);
                Debug.LogError("Viliger  taken");
            }
        }
        else
        {
            Debug.LogError("Empty stack");
        }*/
        

    }
}
