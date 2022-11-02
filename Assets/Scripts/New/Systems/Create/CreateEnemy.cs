using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

public class CreateEnemy : MonoBehaviour
{
    private EnemiesSystem enemiesSystem;
    [Header("Prefabs")]
    public GameObject emptyName;
    public GameObject statSlider;
    public GameObject colorSlider;
    public GameObject lootOption;
    [Header("UI")]
    public TMP_InputField inputNameField;
    public GameObject enemies;
    public GameObject enemyStats;
    public CreateLoot createLoot;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI profitText;
    public ManageEnemy manageEnemy;
    private TextMeshProUGUI lootableName;
    public Image enemyImage;

    [SerializeField] private Sprite enemySprite;
    
 
    //private List<int> 

    [HideInInspector] public float power;
    [HideInInspector] public int profit;




    public Button save;
    public EnemyStatisticsSerializable currentEnemyStatisticsSerializable;
    public Enemy currentEnemy;
  
    private void Awake()
    {
        enemiesSystem = GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>();
    }

    private void Start()
    {
        inputNameField.onValueChanged.AddListener((value) => { CanBeSaved(); });
        foreach (Transform child in enemies.transform)
        {
            Destroy(child.gameObject);
        }
        GameObject empty;
        foreach (GameObject enemy in enemiesSystem.normalEnemies)
        {
            empty = Instantiate(emptyName);
            empty.transform.SetParent(enemies.transform,false);
            empty.GetComponent<Image>().color = Color.Lerp(Color.red, Color.white, 0.8f);
            empty.transform.Find("Text").GetComponent<Text>().text = enemy.name;
            empty.transform.Find("Image").GetComponent<Image>().sprite = enemy.transform.GetComponent<SpriteRenderer>().sprite;
            empty.GetComponent<Button>().onClick.AddListener(() =>
            {
                desctiptionSetAndButton(enemy.GetComponent<Enemy>(),false);
                enemySprite = enemy.transform.GetComponent<SpriteRenderer>().sprite;
                enemyImage.sprite = enemySprite;
            });
         

        }
        foreach (GameObject enemy in enemiesSystem.nightEnemies)
        {
            empty = Instantiate(emptyName);
            empty.transform.SetParent(enemies.transform,false);
            empty.GetComponent<Image>().color = Color.Lerp(Color.blue, Color.white, 0.8f);
            // print(typeof(EnemyStatistics).GetFields().ToString());



            // empty.transform.Find("Main image").GetComponent<Image>().sprite = enemy.;
            // empty.transform.Find("Secound image").GetComponent<Image>().sprite = enemy.sprite2;
            empty.transform.Find("Text").GetComponent<Text>().text = enemy.name;
            empty.transform.Find("Image").GetComponent<Image>().sprite = enemy.transform.GetComponent<Light2D>().lightCookieSprite;
            empty.GetComponent<Button>().onClick.AddListener(() =>
            {
                desctiptionSetAndButton(enemy.GetComponent<Enemy>(),true);
                enemySprite = enemy.transform.GetComponent<Light2D>().lightCookieSprite;
                enemyImage.sprite = enemySprite;
            });
            // empty.GetComponent<Image>().color = setColor(enemy);
            // empty.transform.Find("Cost").transform.Find("Text").GetComponent<Text>().text = enemy.cost.ToString();

        }
        foreach (Transform child in enemyStats.transform)
        {
            Destroy(child.gameObject);
        }

    }
    public void desctiptionSetAndButton(Enemy enemy,bool nightEnemy)
    {
        currentEnemy = enemy;
        CanBeSaved();
        //inputNameField.on
        //enemy = GameObject.Instantiate(enemy);
        //currentEnemy = enemy.gameObject;
        // print(enemy.GetType());
        //  print(enemy.gameObject.name);
        //print(enemy.enemyStatistics.GetType());
        currentEnemyStatisticsSerializable = enemy.enemyStatistics.ToSerializable();
        currentEnemyStatisticsSerializable.nightEnemy = nightEnemy;
        //print(currentEnemyStatisticsSerializable.night);
      //  print(nightEnemy);


        foreach (Transform child in enemyStats.transform)
        {
            Destroy(child.gameObject);
        }
        //print(enemy.enemyStatistics.ToSerializable(enemy.gameObject.name).GetType());
        //print(enemy.enemyStatistics.GetType());
        //print(enemy.enemyStatistics.ToSerializable(enemy.gameObject.name).enemyPrefabName);
        //var ins = ScriptableObject.CreateInstance<typeof(enemy.GetType())>()
       
        foreach (var param in currentEnemyStatisticsSerializable.GetType().GetFields())//.GetConstructors()[0].GetParameters()
        {
            if (param.Name != "color" && param.Name != "lootTable"&& param.Name!="enemyPrefabName" && param.Name != "name" && param.Name != "night" && param.Name != "nightEnemy"&&
                param.Name!="power"&& param.Name!= "inGame")
            {
                GameObject empty;
                // print(param.Name);
                empty = Instantiate(statSlider);
                empty.transform.SetParent(enemyStats.transform,false);
                empty.GetComponent<Image>().color = Color.Lerp(Color.blue, Color.white, 0.8f);
                empty.transform.Find("Text").GetComponent<Text>().text = param.Name;
                empty.transform.Find("Slider").GetComponent<Slider>().value = (float)currentEnemyStatisticsSerializable.GetType().GetField(param.Name).GetValue(currentEnemyStatisticsSerializable);
                SetValue(empty.transform.Find("Slider").GetComponent<Slider>().value, empty, param.Name);
                empty.transform.Find("Slider").GetComponent<Slider>().onValueChanged.AddListener((value) => { SetValue(value, empty,param.Name); });
            }
            else if (param.Name == "color")
            {
                GameObject empty;
                // print(param.Name);
                empty = Instantiate(colorSlider);
                empty.transform.SetParent(enemyStats.transform, false);
                empty.GetComponent<Image>().color = Color.Lerp(Color.blue, Color.white, 0.8f);
                empty.transform.Find("Text").GetComponent<Text>().text = param.Name;

                SetColorValue(empty, param.Name);
                empty.transform.Find("red").GetComponent<Slider>().onValueChanged.AddListener((value) => { SetColorValue(empty, param.Name); });
                empty.transform.Find("green").GetComponent<Slider>().onValueChanged.AddListener((value) => { SetColorValue(empty, param.Name); });
                empty.transform.Find("blue").GetComponent<Slider>().onValueChanged.AddListener((value) => { SetColorValue(empty, param.Name); });
            }
            else if (param.Name == "lootTable")
            {
                GameObject empty;
                // print(param.Name);
                empty = Instantiate(lootOption);
                empty.transform.SetParent(enemyStats.transform, false);
                empty.GetComponent<Image>().color = Color.Lerp(Color.blue, Color.white, 0.8f);
                empty.transform.Find("Text").GetComponent<Text>().text = param.Name;
                lootableName = empty.transform.Find("LootTableName").GetComponent<TextMeshProUGUI>();
                //lootableName.text= currentEnemyStatisticsSerializable.lootTable.name;
                createLoot.currentLootTable = currentEnemyStatisticsSerializable.lootTable.ToNonSerializable();
                empty.transform.Find("New").GetComponent<Button>().onClick.AddListener(()=>createLoot.OpenWindow());
                //  empty.transform.Find("red").GetComponent<Slider>().onValueChanged.AddListener((value) => { SetColorValue(empty); });
                //  empty.transform.Find("green").GetComponent<Slider>().onValueChanged.AddListener((value) => { SetColorValue(empty); });
                //  empty.transform.Find("blue").GetComponent<Slider>().onValueChanged.AddListener((value) => { SetColorValue(empty); });
            }
        }

    }
    public void SetValue(float value, GameObject gameObject,string param)
    {
        // print("tutaj 111 "+value);
        gameObject.transform.Find("Value").GetComponent<Text>().text = value.ToString();
        currentEnemyStatisticsSerializable.GetType().GetField(param).SetValue(currentEnemyStatisticsSerializable,value);
        CanBeSaved();
    }
    public void SetColorValue(GameObject gameObject, string param)
    {
        float[] color = new float[3];
        color[0] = gameObject.transform.Find("red").GetComponent<Slider>().value;
        color[1] = gameObject.transform.Find("green").GetComponent<Slider>().value;
        color[2] = gameObject.transform.Find("blue").GetComponent<Slider>().value;
        enemyImage.color = new Color(color[0],color[1],color[2]);
        currentEnemyStatisticsSerializable.GetType().GetField(param).SetValue(currentEnemyStatisticsSerializable, color);
    }
  /*  public Color setColor(Upgrade enemy)
    {

        if (!enemy.bought)
        {
            return Color.white;
        }
        else
        {
            if (!enemy.activated)
            {
                //  if (enemy is UpPlayerHealth)
                {
                    return Color.grey;
                }
            }
            else
            {
                //  if (enemy is UpPlayerHealth)
                {
                    return Color.green;
                }

            }
        }
    }*/
    public void CloseWindow()
    {
        this.gameObject.SetActive(false);
    }
    public void ChangeLootTable(LootTable lootTable,int profit)
    {
        currentEnemyStatisticsSerializable.lootTable = new LootTableSerializable(lootTable);
        this.profit = profit;
        profitText.text = "Profit: " + profit;
        lootableName.text = currentEnemyStatisticsSerializable.lootTable.name;
        CanBeSaved();
    }
    public void SaveEnemy()
    {
        currentEnemyStatisticsSerializable.name = inputNameField.text;
        currentEnemyStatisticsSerializable.enemyPrefabName = currentEnemy.name;
        currentEnemyStatisticsSerializable.power = (int)power;
        currentEnemyStatisticsSerializable.inGame = false;
        SaveSystem.SaveEnemy(currentEnemyStatisticsSerializable);
        SaveSystem.LoadEnemy();
        manageEnemy.RefreshManageEnemiesList();
        this.gameObject.SetActive(false);
        //currentEnemyStatisticsSerializable = inputField.text;
       // createEnemy.ChangeLootTable(currentLootTable);
        //CloseWindow();
        //SaveSystem.SaveLootTables(currentLootTable);
        // LoadLootTable();
        //  RefreshLootTables();
    }

    public void CanBeSaved()
    {
        GetPower();

        if (string.IsNullOrEmpty(inputNameField.text) || profit > power || string.IsNullOrEmpty(lootableName.text))
        {
            if (string.IsNullOrEmpty(inputNameField.text))
            {
                infoText.text = "Name is empty";
            }
            else if (string.IsNullOrEmpty(lootableName.text))
            {
                infoText.text = "No lootTable";
            }
           
            else
            {
                infoText.text = "Profit " + profit + " can't be >" + power + " power";
            }
            save.interactable = false;
        }
        else
        {
            infoText.text = "";
            save.interactable = true;
        }
    }

        private void GetPower()
        {
            power = 0;
            foreach (var item in currentEnemyStatisticsSerializable.GetType().GetFields())
            {
            if (item.Name != "color" && item.Name != "lootTable" && item.Name != "enemyPrefabName" && item.Name != "name" && item.Name != "night" && item.Name != "nightEnemy" &&
                item.Name != "power" && item.Name != "inGame")
            {
                power += (float)currentEnemyStatisticsSerializable.GetType().GetField(item.Name).GetValue(currentEnemyStatisticsSerializable)*10;
            }
            }
        profitText.text = "Profit: " + profit;
        createLoot.power = (int)power;
        powerText.text = "Power: " + power;
         
        
        }


    }
