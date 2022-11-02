using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class AIViliger : MonoBehaviour
{
    GameObject fireplace;
    public ViligerStatistics viligerStatistics;
    public AstarPath astar;
    public GameObject targetGoTo;
    public Vector3 targetGoPosition;
    public SpriteRenderer[] spriteRenderers;
   // public float speed = 200f;
    public float nextWaypountDistance = 3f;
    

    public int resourcesHeld;
    [HideInInspector] public Sprite resorceImage;
    [HideInInspector] public string resourceName;

    private Animator animator;
    public Animator Animator { get => animator; }

   // private bool switchState = false;
    private ViligearIA viliger;
    [HideInInspector]public bool corutineIsRunning=false;

    public string currentState;
    public StateMachine<AIViliger> stateMachine { get; set; }
    public Stack <State<AIViliger>> stateChain;

    private void Start()
    {
        fireplace = GameObject.FindGameObjectWithTag("Fireplace");
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        stateChain = new Stack<State<AIViliger>>();
        viliger = GetComponent<ViligearIA>();
        stateMachine = new StateMachine<AIViliger>(this);
        stateMachine.ChangeState(VilligerIdle.Instance);
        animator = gameObject.transform.Find("Sprite").GetComponent<Animator>();
        resourcesHeld = 0;
    
    }
    private void Update()
    {
        
        /*if (Time.time > gameTimer + 1)
        {
            gameTimer = Time.time;
            seconds++;
            Debug.Log(seconds);
        }
        if (seconds == 1)
        {
            seconds = 0;
            switchState = !switchState;
        }*/
        //if(!bre)
        stateMachine.Update();
        currentState = stateMachine.curentState.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      /*  if (collision.CompareTag("Enemies"))
        {
            //bre = true;
            Debug.LogError("Attttack");
            targetGoTo = GameObject.FindGameObjectWithTag("Fireplace");
            stateChain.Clear();
            //stateChain.Push(VilligerLookingForTool.Instance);
            stateMachine.ChangeState(ViligerGo.Instance);
            //bre = false;
        }*/
    }
    public void Night()
    {
       // Debug.LogError("Night");
        stateChain.Push(stateMachine.curentState);
        stateChain.Push(ViligerNight.Instance);
        targetGoPosition = GameObject.FindGameObjectWithTag("Fireplace").GetComponent<Fireplace>().PositionInCircle();
        stateMachine.ChangeState(ViligerGoTPosition.Instance);
        //fireplace.GetComponent<Fireplace>().viligersNight.Add(this.gameObject);

 
    }
    public void Day()
    {
        // fireplace.GetComponent<Fireplace>().viligersNight.Remove(this.gameObject);
        //  Debug.LogError("Day");
        // stateChain.Clear();
        if (currentState == "ViligerNight")
        {
            try
            {
                stateMachine.ChangeState(stateChain.Pop());
            }
            catch (System.Exception)
            {
                if (GetComponent<Tool>().Item != null)
                {
                    if (resourcesHeld > 0)
                    {
                        Animator.SetBool("carry", true);
                    }
                    stateMachine.ChangeState(VilligerLookingForJobWithTool.Instance);
                }
                else
                {
                    //fireplace.GetComponent<Fireplace>().viligers.AddFirst(this.gameObject);
                }
            }
        }
       
            
            
        
    }
    public IEnumerator AttackCo()
    {
        /*int i = 0;
        while (tree.health >= 0 && i < 100)
        {*/
        corutineIsRunning = true;
            animator.SetBool("attacking", true);
            yield return null;
            yield return new WaitForSeconds(.3f);

            animator.SetBool("attacking", false);


            //yield return new WaitForSeconds(.3f);
            ///tree.Attack(1);
            //i++;
            yield return new WaitForSeconds(3f);
        corutineIsRunning = false;
       // }

    }
    public IEnumerator TakeCo()
    {
       
        corutineIsRunning = true;
        
        animator.SetBool("carry", true);
        yield return null;
        yield return new WaitForSeconds(1f);
        transform.Find("Sprite").Find("Received Item").GetComponent<SpriteRenderer>().sprite = resorceImage;
        //Destroy(target1.gameObject);
        yield return new WaitForSeconds(.2f);
        
       
        corutineIsRunning = false;
       

    }
    public IEnumerator TakeToolCo()
    {

        corutineIsRunning = true;

        animator.SetBool("receiveItem", true);
        transform.Find("Tool").GetComponent<SpriteRenderer>().sprite = targetGoTo.GetComponent<Tool>().Item.itemImage;
       //yield return null;
        yield return new WaitForSeconds(.2f);
        //yield return new WaitForSeconds(.3f);
        
        //yield return new WaitForSeconds(3f);
        //transform.FindChild("Received Item").GetComponent<SpriteRenderer>().sprite = null;
        gameObject.GetComponent<Tool>().Item = targetGoTo.GetComponent<Tool>().Item;
        yield return null;
        Destroy(targetGoTo.gameObject);
        yield return null;
        animator.SetBool("receiveItem", false);
        yield return new WaitForSeconds(.2f);
        
       
        


        corutineIsRunning = false;


    }
    public IEnumerator TakeCoOf()
    {

        corutineIsRunning = true;
        yield return null;
        animator.SetBool("carry", false);
        if (GetComponent<Tool>().Item.itemName == "Axe") { 
        GameObject.FindGameObjectWithTag("GUI").GetComponent<Gui>().ModifyWood(resourcesHeld);
        }
        else if(GetComponent<Tool>().Item.itemName == "Pick"){
         GameObject.FindGameObjectWithTag("GUI").GetComponent<Gui>().ModifyStone(resourcesHeld);
        }
        resourcesHeld = 0;
        resourceName = null;
        resorceImage = null;
        yield return null;
        yield return new WaitForSeconds(.4f);
        transform.Find("Sprite").Find("Received Item").GetComponent<SpriteRenderer>().sprite = null;
        yield return new WaitForSeconds(.4f);

       
        corutineIsRunning = false;
    

    }
    public IEnumerator WaitForS(float sec)
    {


        corutineIsRunning = true;
        yield return new WaitForSeconds(sec);


        corutineIsRunning = false;


    }
    public IEnumerator CantReachTarget()
    {


        corutineIsRunning = true;
        this.gameObject.transform.Find("Dialog").gameObject.SetActive(true);
        this.gameObject.transform.Find("Dialog").transform.Find("NoWay").gameObject.SetActive(true);
        animator.SetBool("moving", false);

       

        
        
        
        yield return new WaitForSeconds(5f);
        this.gameObject.transform.Find("Dialog").transform.Find("NoWay").gameObject.SetActive(false);
        this.gameObject.transform.Find("Dialog").gameObject.SetActive(false);
        stateMachine.ChangeState(ViligerGo.Instance);
        //yield return new WaitForSeconds(30f);
        corutineIsRunning = false;


    }
    public IEnumerator LookingforTarget()
    {


        corutineIsRunning = true;
        this.gameObject.transform.Find("Dialog").gameObject.SetActive(true);
        this.gameObject.transform.Find("Dialog").transform.Find("NoTarget").gameObject.SetActive(true);

        animator.SetBool("moving", false);






        yield return new WaitForSeconds(5f);
        this.gameObject.transform.Find("Dialog").transform.Find("NoTarget").gameObject.SetActive(false);
        this.gameObject.transform.Find("Dialog").gameObject.SetActive(false);
        stateMachine.ChangeState(ViligerGo.Instance);
        //yield return new WaitForSeconds(30f);
        corutineIsRunning = false;


    }
    public void DestroyOb(GameObject a)
    {
        Destroy(a.gameObject);
    }
}
