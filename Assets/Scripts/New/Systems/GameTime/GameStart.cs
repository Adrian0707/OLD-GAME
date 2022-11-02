using System.Collections;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public Signal2 startGameSignal;
    public BuildingPlacement buildingPlacement;
    public GameObject Fireplace;
    public GameObject Player;
    public GameObject[] objectsSpawnedAtStart;
    public StrategyInfoSystem infoSystem;
    void Start()
    {
       // Debug.LogError(GameObject.FindGameObjectWithTag("Fireplace"));
    }
    private void Awake()
    {

        // SaveSystem.LoadLootTables();
        SaveSystem.LoadCoins();
        SaveSystem.LoadEnemy();
        SaveSystem.LoadUpgrades();
        startGameSignal.Raise();
        StartCoroutine(StartCo());
    }

    // Update is called once per frame
    void Update()
    {
       /* if (!GameObject.FindGameObjectWithTag("Fireplace")) { 
            buildingPlacement.SetItem(basicObjects[0]);
        }
        else if (GameObject.FindGameObjectWithTag("Fireplace"))
        {
            buildingPlacement.SetItem(basicObjects[1]);
        }*/
        

    }
    IEnumerator StartCo()
    {
        yield return null;
        startGameSignal.Raise();
        buildingPlacement.PutItem(Fireplace);
        
        while (!buildingPlacement.hasPlaced)
            {
          //  startGameSignal.Raise();
            yield return null;
            }
        Fireplace.transform.position = Fireplace.transform.position - new Vector3(0, 0, 1);
        /* Fireplace.SetActive(false);
         Fireplace.SetActive(true);*/
        //yield return null;
        Fireplace.GetComponent<Fireplace>().makepath();
        //Debug.LogError(GameObject.FindGameObjectWithTag("Fireplace"));
        Player.gameObject.transform.position = Fireplace.transform.position+new Vector3(0,-2,0);
        int i = 0;
        foreach (GameObject item in objectsSpawnedAtStart)
        {
            GameObject.Instantiate(item, Fireplace.transform.position + new Vector3(0, -2, 0), Quaternion.identity);
        }
       // Player.GetComponent<Rigidbody2D>().isKinematic = false;
        Player.SetActive(true);
        Fireplace.GetComponent<Fireplace>().enabled = true;
       // Player.SetActive(true);

        }
    }

