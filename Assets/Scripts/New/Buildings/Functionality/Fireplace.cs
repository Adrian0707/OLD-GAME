using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour
{
    //private AstarPath path;
    public FireplaceStats fireplaceStats;
    public GameObject markRU;
    public GameObject markLU;
    public GameObject markRD;
    public GameObject markLD;
    public GameObject marker;


    public List<GameObject> trees;
    public List<GameObject> stones;
    public int position;
    //public List<GameObject> viligersNight = new List<GameObject>();
    private Pathfinding.GridGraph gGraph;
    Pathfinding.GridGraph g;
    public float radius;
    //private List<GameObject> marks;
    Transform myPosition;
    //public GameObject instace;
    public LinkedList<GameObject> viligers= new LinkedList<GameObject>();
   // public Stack<GameObject> Tools = new Stack<GameObject>();
    private int viligersNumber;
    private Gui strategyGui;
    public Signal2 noResourceSignal;
    public Signal2 updateViligersInfo;
    public CapsuleCollider2D myCollider;
    public bool isNight;
    public bool nightCircle;
    public void makepath()
    {
        FindAllStones();
        FindAllTrees();
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Mark"))
        {
            Destroy(item);
        }
        int msize =(int)fireplaceStats.maxDistance.Value;
         gGraph = AstarPath.active.data.gridGraph;   
        gGraph.width = msize;
        gGraph.depth = msize;
        gGraph.UpdateSizeFromWidthDepth();
        markLD.SetActive(true);
        markLU.SetActive(true);
        markRD.SetActive(true);
        markRU.SetActive(true);
        markLD.transform.position = transform.position+ new Vector3(-msize /  4 + 0.2f, - msize / 4 + 1.5f);
        markLU.transform.position = transform.position + new Vector3(-msize / 4 + 0.2f, msize / 4 + 1);
        markRD.transform.position = transform.position + new Vector3(msize / 4 - 0.2f, -msize / 4 + 1.5f);
        markRU.transform.position = transform.position + new Vector3(msize / 4 - 0.2f, msize / 4 + 1);
        for (int i = -msize / 4; i < msize / 4; i += 10)
        {
            GameObject mark = GameObject.Instantiate(marker, transform.position + new Vector3(-msize / 4 + 0.2f, i), Quaternion.identity);
            mark.SetActive(true);
            mark.transform.parent = this.transform;    
        }
        for (int i = -msize / 4; i < msize / 4; i += 10)
        {
            GameObject mark = GameObject.Instantiate(marker, transform.position + new Vector3(msize / 4 + 0.2f, i), Quaternion.identity);
            mark.SetActive(true);
            mark.transform.parent = this.transform;
        }
        for (int i = -msize / 4; i < msize / 4; i += 10)
        {
            GameObject mark = GameObject.Instantiate(marker, transform.position + new Vector3(i, msize / 4 + 0.2f), Quaternion.identity);
            mark.SetActive(true);
            mark.transform.parent = this.transform;
        }
        for (int i = -msize / 4; i < msize / 4; i += 10)
        {
            GameObject mark = GameObject.Instantiate(marker, transform.position + new Vector3(i,- msize / 4 + 0.2f), Quaternion.identity);
            mark.SetActive(true);
            mark.transform.parent = this.transform;
        }

        gGraph.center = transform.position;
        AstarPath.active.Scan();
    }
    void Start()
    {
        position = 0;
        nightCircle = false;
        isNight = false;
        myCollider.enabled = true;
        makepath();
        //gameObject.GetComponent<Menu>().enabled = true;
        viligersNumber = 0;
        myPosition = transform;
        strategyGui = GameObject.FindGameObjectWithTag("GUI").GetComponent<Gui>();

        //viligers = new Stack<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (viligersNumber != viligers.Count)
        {
           // myPosition = transform;
            viligersNumber = viligers.Count;
            instantiateInCircle(viligers, this.gameObject.transform.position, viligersNumber,radius);
            
        }
        if (viligersNumber > 0&& !isNight)
        {
            foreach (GameObject a in FindAllFreeTools())
            {
                
                takeTool(a);
            }
        }
       /* if (isNight && !nightCircle && viligersNight.Count == GameObject.FindGameObjectsWithTag("Viliger").Length)
        {
            instantiateInCircle(viligersNight, this.gameObject.transform.position, viligersNumber, radius);
            nightCircle = true;
        }*/
    }
    public void instantiateInCircle(LinkedList<GameObject> obj, Vector3 location, int howMany,float rad)
    {
        int i = 0;
       foreach(GameObject viliger in viligers)
        {
            float radius = rad;
            float angle = i * Mathf.PI * 2f / howMany;
            Vector3 newPos =location+ new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius,0);
            
            if (viliger != null)
            {
                viliger.GetComponent<AIViliger>().targetGoPosition = newPos;
                viliger.GetComponent<AIViliger>().stateMachine.ChangeState(ViligerGoTPosition.Instance);
            }

            i++;
        }
    }
    public Vector3 PositionInCircle()
    {
      //  print("koooolkoo");

        //float radius = ;
            position++;
            float angle = position * Mathf.PI * 2f / GameObject.FindGameObjectsWithTag("Viliger").Length;
            return this.transform.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);

            

            
        
    }
    void takeTool(GameObject a)
    {/*
        GameObject a=GameObject.FindGameObjectWithTag("stuff");
        if (a != null&&viligersNumber>0)
        {*/
        if (viligers.Count > 0)
        {
            GameObject b = viligers.First.Value;
            viligers.RemoveFirst();
            a.GetComponent<Tool>().taked = true;
            //Destroy(b.GetComponent<AIViliger>().target1.gameObject);
            if (b != null)
            {
                b.GetComponent<AIViliger>().targetGoTo = a;
                b.GetComponent<AIViliger>().stateChain.Push(ViligerTakeTool.Instance);
                b.GetComponent<AIViliger>().stateMachine.ChangeState(ViligerGo.Instance);
            }
        }
        //}

    }
    Stack<GameObject> FindAllFreeTools()
    {
        Stack<GameObject> freeTool = new Stack<GameObject>();
        GameObject[] all = GameObject.FindGameObjectsWithTag("Tool");
            foreach(GameObject a in all)
        {
            if (a.GetComponent<Tool>().taked||!IsInBounds(a.transform)) { }
            else
            {
                freeTool.Push(a);
            }
        }
        return freeTool;
    }
    public void CreateViliger(GameObject o)
    {
        if (GameObject.FindGameObjectsWithTag("Viliger").Length < fireplaceStats.maxViligers.Value)
        {


            if (strategyGui.gold >= 3)
            {
                GameObject newCharacter = Instantiate(o);
                newCharacter.transform.position = transform.position + new Vector3(0, -2, 1);
                newCharacter.gameObject.transform.parent = GameObject.FindGameObjectWithTag("Characters").transform;
                strategyGui.ModifyGold(-3);
                updateViligersInfo.Raise();

            }
            else
            {
                noResourceSignal.Raise();
            }
        }
        else
        {
            noResourceSignal.Raise();
        }
    }
   public bool IsInBounds(Transform obj)
    {
       
       
        if (obj.position.x > transform.position.x - fireplaceStats.maxDistance.Value / 4 && obj.position.x < transform.position.x + fireplaceStats.maxDistance.Value / 4 &&
            obj.position.y > transform.position.y - fireplaceStats.maxDistance.Value / 4 && obj.position.y < transform.position.y + fireplaceStats.maxDistance.Value / 4)
            return true;
        else
            return false;
    }
    public void FindAllTrees()
    {
        List<GameObject> treesInBounds = new List<GameObject>();
            GameObject[] trees= GameObject.FindGameObjectsWithTag("Tree");
        foreach (GameObject tree in trees)
        {
            if (IsInBounds(tree.transform))
            {
                treesInBounds.Add(tree);
            }
        }
        this.trees = treesInBounds;
    }
    public void FindAllStones()
    {
        List<GameObject> stonesInBounds = new List<GameObject>();
        GameObject[] stones = GameObject.FindGameObjectsWithTag("Stone");
        foreach (GameObject stone in stones)
        {
            if (IsInBounds(stone.transform))
            {
                stonesInBounds.Add(stone);
            }
        }
        this.stones = stonesInBounds;
    }
    public void Day()
    {
        position = 0;
        instantiateInCircle(viligers, this.gameObject.transform.position, viligersNumber, radius);
        nightCircle = false;
        isNight = false;
    }
    public void Night()
    {

        isNight = true;
    }
}
