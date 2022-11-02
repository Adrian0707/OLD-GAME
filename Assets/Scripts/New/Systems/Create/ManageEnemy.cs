using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

public class ManageEnemy : MonoBehaviour
{
    private EnemiesSystem enemiesSystem;
    [Header("Prefabs")]
    public GameObject emptyName;
    public GameObject emptyNameAndDeleteButton;
    [Header("UI")]

    public GameObject enemiesNotInGame;
    public GameObject enemiesInGame;


    //public CreateLoot createLoot;
    public TextMeshProUGUI infoText;

    public GameObject createEnemy;
   // private TextMeshProUGUI lootableName;
    public Image enemyImage;

    public GameObject addRemoveButton;
    public GameObject createEnemyButton;
    public GameObject deleteButton;

   // public EnemyStatisticsSerializable currentEnemyStatisticsSerializable;
    public GameObject currentEnemy;
   
    private void Awake()
    {
        enemiesSystem = GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>();
    }

    private void Start()
    {
       
        RefreshManageEnemiesList();
       /* foreach (Transform child in enemyStats.transform)
        {
            Destroy(child.gameObject);
        }*/

    }
    public void RefreshManageEnemiesList()
    {
        deleteButton.transform.Find("Text").GetComponent<Text>().text = "Delete";
        deleteButton.GetComponent<Button>().enabled = false;



        addRemoveButton.transform.Find("Text").GetComponent<Text>().text = "Add/Remove";
        addRemoveButton.GetComponent<Button>().enabled = false;
        addRemoveButton.GetComponent<Button>().onClick.RemoveAllListeners();


        foreach (Transform child in enemiesInGame.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in enemiesNotInGame.transform)
        {
            Destroy(child.gameObject);
        }
       // print("ooo" + enemiesSystem.playerNormalEnemies.Count);
        GameObject empty;
        foreach (GameObject enemy in enemiesSystem.playerNormalEnemies)
        {
            if (enemy.GetComponent<Enemy>().enemyStatistics.inGame)
            {
                empty = Instantiate(emptyName);
                empty.transform.SetParent(enemiesInGame.transform,false);
                empty.GetComponent<Button>().onClick.AddListener(() =>
                {
                    SelectInGameEnemy(enemy, enemy.transform.GetComponent<SpriteRenderer>().sprite);
                    //  desctiptionSetAndButton(enemy.GetComponent<Enemy>(),false);
                    // enemySprite = enemy.transform.GetComponent<SpriteRenderer>().sprite;
                    //  enemyImage.sprite = enemySprite;
                });
            }
            else
            {
               
                empty = Instantiate(emptyName);
                empty.transform.SetParent(enemiesNotInGame.transform,false);
                empty.GetComponent<Button>().onClick.AddListener(() =>
                {
                    SelectNotInGameEnemy(enemy, enemy.transform.GetComponent<SpriteRenderer>().sprite);
                    //  desctiptionSetAndButton(enemy.GetComponent<Enemy>(),false);
                    // enemySprite = enemy.transform.GetComponent<SpriteRenderer>().sprite;
                    //  enemyImage.sprite = enemySprite;
                });
            }
            empty.GetComponent<Image>().color = Color.Lerp(Color.red, Color.white, 0.8f);
            empty.transform.Find("Text").GetComponent<Text>().text = "Name: " + enemy.name;
            empty.transform.Find("Image").GetComponent<Image>().sprite = enemy.transform.GetComponent<SpriteRenderer>().sprite;
            empty.transform.Find("Image").GetComponent<Image>().color = enemy.GetComponent<Enemy>().enemyStatistics.color;
        }





        foreach (GameObject enemy in enemiesSystem.playerNightEnemies)
        {
            if (enemy.GetComponent<Enemy>().enemyStatistics.inGame)
            {
                empty = Instantiate(emptyName);
                empty.transform.SetParent(enemiesInGame.transform,false);
                empty.GetComponent<Button>().onClick.AddListener(() =>
                {
                    SelectInGameEnemy(enemy, enemy.transform.GetComponent<Light2D>().lightCookieSprite);
                    //  desctiptionSetAndButton(enemy.GetComponent<Enemy>(),false);
                    // enemySprite = enemy.transform.GetComponent<SpriteRenderer>().sprite;
                    //  enemyImage.sprite = enemySprite;
                });
            }
            else
            {
                empty = Instantiate(emptyName);
                empty.transform.SetParent(enemiesNotInGame.transform,false);
                empty.GetComponent<Button>().onClick.AddListener(() =>
                {
                    SelectNotInGameEnemy(enemy, enemy.transform.GetComponent<Light2D>().lightCookieSprite);
                    //  desctiptionSetAndButton(enemy.GetComponent<Enemy>(),false);
                    // enemySprite = enemy.transform.GetComponent<SpriteRenderer>().sprite;
                    //  enemyImage.sprite = enemySprite;
                });
            }
            empty.GetComponent<Image>().color = Color.Lerp(Color.blue, Color.white, 0.8f);
            empty.transform.Find("Text").GetComponent<Text>().text = "Name: " + enemy.name;
            empty.transform.Find("Image").GetComponent<Image>().sprite = enemy.transform.GetComponent<Light2D>().lightCookieSprite;
            empty.transform.Find("Image").GetComponent<Image>().color = enemy.GetComponent<Enemy>().enemyStatistics.color;

        }
    }
    public void SelectNotInGameEnemy(GameObject enemy, Sprite spriteEnemy)
    {
        currentEnemy = enemy;
        addRemoveButton.transform.Find("Text").GetComponent<Text>().text = "Add " + enemy.name;
        addRemoveButton.GetComponent<Button>().enabled = true;
        addRemoveButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            AddToGame();
        });
        enemyImage.sprite = spriteEnemy;
        enemyImage.color = enemy.GetComponent<Enemy>().enemyStatistics.color;
        infoText.text = enemy.name + " Power:" + enemy.GetComponent<Enemy>().enemyStatistics.power;
        deleteButton.GetComponent<Button>().enabled = true;
        deleteButton.transform.Find("Text").GetComponent<Text>().text = "Delete " + enemy.name;
    }
    public void SelectInGameEnemy(GameObject enemy, Sprite spriteEnemy)
    {
        currentEnemy = enemy;
        addRemoveButton.transform.Find("Text").GetComponent<Text>().text = "Remove " + enemy.name;
        addRemoveButton.GetComponent<Button>().enabled = true;
        addRemoveButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            RemoveFromGame();
        });
        enemyImage.sprite = spriteEnemy;
        enemyImage.color = enemy.GetComponent<Enemy>().enemyStatistics.color;
        infoText.text = enemy.name + " Power:" + enemy.GetComponent<Enemy>().enemyStatistics.power;
        deleteButton.GetComponent<Button>().enabled = false;
        deleteButton.transform.Find("Text").GetComponent<Text>().text = "Delete ";
    }
    public void AddToGame()
    {
        currentEnemy.GetComponent<Enemy>().enemyStatistics.inGame = true;
      
        SaveSystem.SaveEnemies();
        SaveSystem.LoadEnemy();
        RefreshManageEnemiesList();
    }
    public void RemoveFromGame()
    {
        currentEnemy.GetComponent<Enemy>().enemyStatistics.inGame = false;
        SaveSystem.SaveEnemies();
        SaveSystem.LoadEnemy();
        RefreshManageEnemiesList();
    }
    public void Delete()
    {
        if (enemiesSystem.playerNightEnemies.Contains(currentEnemy))
        {
            enemiesSystem.playerNightEnemies.Remove(currentEnemy);
        }
        else
        {
            enemiesSystem.playerNormalEnemies.Remove(currentEnemy);
        }
        if (enemiesSystem.playerNightEnemyStatistics.Contains(currentEnemy.GetComponent<Enemy>().enemyStatistics))
        {
            enemiesSystem.playerNightEnemyStatistics.Remove(currentEnemy.GetComponent<Enemy>().enemyStatistics);
        }
        else
        {
            enemiesSystem.playerNormalEnemyStatistics.Remove(currentEnemy.GetComponent<Enemy>().enemyStatistics);
        }
        SaveSystem.SaveEnemies();
        SaveSystem.LoadEnemy();
        RefreshManageEnemiesList();
    }
    public void OpenWindow()
    {
        createEnemy.SetActive(true);
    }
    /* public void desctiptionSetAndButton(Enemy enemy,bool nightEnemy)
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
   */
    /*  public void SetColorValue(GameObject gameObject, string param)
      {
          float[] color = new float[3];
          color[0] = gameObject.transform.Find("red").GetComponent<Slider>().value;
          color[1] = gameObject.transform.Find("green").GetComponent<Slider>().value;
          color[2] = gameObject.transform.Find("blue").GetComponent<Slider>().value;
          enemyImage.color = new Color(color[0],color[1],color[2]);
          currentEnemyStatisticsSerializable.GetType().GetField(param).SetValue(currentEnemyStatisticsSerializable, color);
      }*/

    public void CloseWindow()
    {
        this.gameObject.SetActive(false);
    }
    public void SaveEnemy()
    {
       // currentEnemyStatisticsSerializable.name = inputNameField.text;
       // currentEnemyStatisticsSerializable.enemyPrefabName = currentEnemy.name;
       // currentEnemyStatisticsSerializable.power = (int)power;
      //  currentEnemyStatisticsSerializable.inGame = false;
      //  SaveSystem.SaveEnemy(currentEnemyStatisticsSerializable);
     //   SaveSystem.LoadEnemy();
        //currentEnemyStatisticsSerializable = inputField.text;
       // createEnemy.ChangeLootTable(currentLootTable);
        //CloseWindow();
        //SaveSystem.SaveLootTables(currentLootTable);
        // LoadLootTable();
        //  RefreshLootTables();
    }
}
