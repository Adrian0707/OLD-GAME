using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class WorldUpgrades : MonoBehaviour
{
    /* public UpgradeSystem upgradeSystem;
     public GameObject emptyUpgrade;
     public GameObject content;
     public TextMeshProUGUI description;
     public TextMeshProUGUI buttonText;
     public Button descButton;*/
    //public Text price;
    //public GameObject priceElem;
    private EnemiesNightAtack nightAtack;
    private DayNightSystem dayNightSystem;
    public Slider diameter;
    public Slider nearestNode;
    public Slider heuristicScale;
    public GameObject dangerEquation;
    public Slider daySlider;
    public Slider nightSider;

    public GameObject worldStat;
    public WorldGenStatistics worldGenStatistics;
    public WorldGenStatistics modWorldStatistics;
    public GameObject content;

    private InputField equation;
    private Text dangerEquationInfo;
    private Button saveDangerEqButton;
    //private bool equationCorrect;
   /* void Start()
    {
        equation = dangerEquation.GetComponentInChildren<InputField>();
        dangerEquationInfo = dangerEquation.transform.Find("Info").GetComponent<Text>();
        saveDangerEqButton = dangerEquation.GetComponentInChildren<Button>();
       
        nightAtack = GameObject.FindGameObjectWithTag("DayNightSystem").GetComponent<EnemiesNightAtack>();
        equation.text= nightAtack.difficultyEquation;
        IsEquationCorrect(nightAtack.difficultyEquation);

       Pathfinding.GridGraph gGraph = AstarPath.active.data.gridGraph;
        diameter.value = gGraph.collision.diameter;
        nearestNode.value = gGraph.active.maxNearestNodeDistance;
        heuristicScale.value = gGraph.active.heuristicScale;
       
    }*/
    private void OnEnable()
    {
        AddWorldStats();
        if (dangerEquation != null)
        {
            equation = dangerEquation.GetComponentInChildren<InputField>();
            dangerEquationInfo = dangerEquation.transform.Find("Info").GetComponent<Text>();
            saveDangerEqButton = dangerEquation.GetComponentInChildren<Button>();

            nightAtack = GameObject.FindGameObjectWithTag("DayNightSystem").GetComponent<EnemiesNightAtack>();
            dayNightSystem = GameObject.FindGameObjectWithTag("DayNightSystem").GetComponent<DayNightSystem>();
            equation.text = nightAtack.difficultyEquation;
            IsEquationCorrect(nightAtack.difficultyEquation);

            daySlider.value = dayNightSystem.dayTime;
            nightSider.value = dayNightSystem.nightTime;

            Pathfinding.GridGraph gGraph = AstarPath.active.data.gridGraph;
            diameter.value = gGraph.collision.diameter;
            nearestNode.value = gGraph.active.maxNearestNodeDistance;
            heuristicScale.value = gGraph.active.heuristicScale;
        }
    }
    public void AddWorldStats()
    {
        foreach (Transform item in content.transform)
        {
            if(item.gameObject.name== "ToDestroy")
            {
                Destroy(item.gameObject);
            }
        }
        foreach (var param in modWorldStatistics.GetType().GetFields())
        {
                GameObject empty;
                // print(param.Name);
                empty = Instantiate(worldStat);
                empty.name = "ToDestroy";
                empty.transform.SetParent(content.transform, false);
                //empty.GetComponent<Image>().color = Color.Lerp(Color.blue, Color.white, 0.8f);
                empty.transform.Find("Text").GetComponent<Text>().text = param.Name;
                empty.transform.Find("Slider").GetComponent<Slider>().value = (int)modWorldStatistics.GetType().GetField(param.Name).GetValue(modWorldStatistics);
                empty.transform.Find("Reset").GetComponent<Button>().onClick.AddListener(() => ResetValue(param, empty.transform.Find("Slider").GetComponent<Slider>()));
                empty.transform.Find("Save").GetComponent<Button>().onClick.AddListener(() => SaveValue(modWorldStatistics.GetType().GetField(param.Name),
                    (int)empty.transform.Find("Slider").GetComponent<Slider>().value));
              //  SetValue(empty.transform.Find("Slider").GetComponent<Slider>().value, empty, param.Name);
              // empty.transform.Find("Slider").GetComponent<Slider>().onValueChanged.AddListener((value) => { SetValue(value, empty, param.Name); });
        }
        }
    public void SaveValue(FieldInfo field, int value)
    {
        field.SetValue(modWorldStatistics, value);
    }
    public void ResetValue(FieldInfo field,Slider slider)
    {
        field.SetValue(modWorldStatistics, worldGenStatistics.GetType().GetField(field.Name).GetValue(worldGenStatistics));
        slider.value = (int)worldGenStatistics.GetType().GetField(field.Name).GetValue(worldGenStatistics);
    }
    public void CutCorners(int Value)
    {
        Pathfinding.GridGraph gGraph = AstarPath.active.data.gridGraph;
        if (Value == 0)
        {
            gGraph.cutCorners = true;
        }
        else if (Value == 1)
        {
            gGraph.cutCorners = false;
        }
    }
    public void Heuristic(int Value)
    {
        Pathfinding.GridGraph gGraph = AstarPath.active.data.gridGraph;
        if (Value == 0)
        {
            gGraph.active.heuristic = Pathfinding.Heuristic.Euclidean;
        }
        else if (Value == 1)
        {
            gGraph.active.heuristic = Pathfinding.Heuristic.DiagonalManhattan;
        }
        else if (Value == 2)
        {
            gGraph.active.heuristic = Pathfinding.Heuristic.Manhattan;
        }
        else if (Value == 3)
        {
            gGraph.active.heuristic = Pathfinding.Heuristic.None;
        }
    }
    public void HeuristicScale(Slider slider)
    {
        Pathfinding.GridGraph gGraph = AstarPath.active.data.gridGraph;
        gGraph.active.heuristicScale = slider.value;
    }
    public void NarestNode(Slider slider)
    {
        Pathfinding.GridGraph gGraph = AstarPath.active.data.gridGraph;
        gGraph.active.maxNearestNodeDistance = slider.value;
    }
    public void Diameter(Slider slider)
    {
        Pathfinding.GridGraph gGraph = AstarPath.active.data.gridGraph;
        gGraph.collision.diameter = slider.value;
    }
    public void IsEquationCorrect(string equation)
    {
        if (nightAtack.IfEquasionIsCorrect(equation)&& nightAtack.difficultyEquation != equation)
        {
            dangerEquationInfo.text = "Equation is Correct you can save it";
            dangerEquationInfo.color = Color.Lerp(Color.gray, Color.green, 0.5f);
            saveDangerEqButton.interactable = true;
        }
        else if(!nightAtack.IfEquasionIsCorrect(equation))
        {
            dangerEquationInfo.text = "Equation is incrorrect maybe you need to use 'DAY' as variable";
            dangerEquationInfo.color = Color.Lerp(Color.gray, Color.red, 0.5f);
            saveDangerEqButton.interactable = false;
        }
        else//(nightAtack.difficultyEquation == equation)
        {
            dangerEquationInfo.text = "";
            saveDangerEqButton.interactable = false;
        }
    }
    public void SaveEquation()
    {
        nightAtack.difficultyEquation = equation.text;
        dangerEquationInfo.text = "";
        saveDangerEqButton.interactable = false;
    }

    public void DayTime(Slider slider)
    {
        // Pathfinding.GridGraph gGraph = AstarPath.active.data.gridGraph;
        dayNightSystem.dayTime = (int)slider.value;
    }
    public void NightTime(Slider slider)
    {
        // Pathfinding.GridGraph gGraph = AstarPath.active.data.gridGraph;
        dayNightSystem.nightTime = (int)slider.value;
    }
}
    /*private void OnEnable()
    {
        foreach(Transform child in content.transform)
        {
           Destroy(child.gameObject);
        }
        GameObject empty;
        foreach(Upgrade up in upgradeSystem.worldUpgrades)
        {
            empty =Instantiate(emptyUpgrade);
            empty.transform.SetParent(content.transform,false);
            empty.transform.Find("Main image").GetComponent<Image>().sprite = up.sprite;
            empty.transform.Find("Secound image").GetComponent<Image>().sprite = up.sprite2;
            empty.transform.Find("Name").transform.Find("Text").GetComponent<Text>().text = up.name;
            empty.GetComponent<Button>().onClick.AddListener(() => desctiptionSetAndButton(up));
            empty.GetComponent<Image>().color = setColor(up);
            empty.transform.Find("Cost").transform.Find("Text").GetComponent<Text>().text = up.cost.ToString();
            if (up.bought)
            {
                empty.transform.Find("Cost").gameObject.SetActive(false);
            }
        }

    }
    public void desctiptionSetAndButton(Upgrade up)
    {
        descButton.onClick.AddListener(() => empty());
        
        description.SetText(up.description);
        descButton.enabled = true;
        if (!up.bought)
        {
            if (up is UpPlayer)
            {
                buttonText.SetText("Buy");
                descButton.onClick.AddListener(()=>up.buy()); 
            }
        }
        else
        {
            if (!up.activated)
            {
                if (up is UpPlayer)
                {
                    buttonText.SetText("Activate");
                    descButton.onClick.AddListener(() => up.Equip());
                }
            }
            else
            {
                    if (up is UpPlayer)
                    {
                        buttonText.SetText("Deactivate");
                        descButton.onClick.AddListener(() => up.Unequip());
                    }
           
            }
        }
        descButton.onClick.AddListener(() => OnEnable());
        descButton.onClick.AddListener(() => SaveSystem.SaveUpgrades());
    }
    public void empty()
    {
        description.SetText("");
        buttonText.SetText("");
        descButton.onClick.RemoveAllListeners();
        descButton.onClick.AddListener(() => OnEnable());
        descButton.enabled = false;
    }
    public Color setColor(Upgrade up)
    {
        
        if (!up.bought)
        {
            return Color.white;
        }
        else
        {
            if (!up.activated)
            {
              //  if (up is UpPlayerHealth)
                {
                    return Color.grey;
                }
            }
            else
            {
              //  if (up is UpPlayerHealth)
                {
                    return Color.green;
                }

            }
        }
    }*/


