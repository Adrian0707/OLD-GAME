using UnityEngine;


public class Blacksmith : MonoBehaviour
{
    public BlacksmithStats blacksmithStats;
  /*  public GameObject emptyMenuOption;
    public Transform content;
    GameObjectsSystem gameObjectsSystem;*/
    public Signal2 noResourcesSignal;
   // public BoxCollider2D myCollider;
    
    Gui strategyGui;
    // Start is called before the first frame update
    void Start()
    {
       // myCollider.enabled = true;
        gameObject.GetComponent<Menu>().enabled = true;
        strategyGui = GameObject.FindGameObjectWithTag("GUI").GetComponent<Gui>();
       /* gameObjectsSystem = GameObject.FindGameObjectWithTag("GameObjectsSystem").GetComponent<GameObjectsSystem>();*/
    }

    // Update is called once per frame
   
    public void CreateTool(GameObject o)
    {
        if (strategyGui.stone >= 5 && strategyGui.wood >= 10)
        {
            strategyGui.ModifyStone((int)(-5*(1-(blacksmithStats.decreseCosts.Value/100))));
           // Debug.LogError((int)(-5 * (1 - (blacksmithStats.decreseCosts.Value / 100))));
            strategyGui.ModifyWood((int)(-10 * (1 - (blacksmithStats.decreseCosts.Value / 100))));
           // Debug.LogError((int)(-10 * (1 - (blacksmithStats.decreseCosts.Value / 100))));
            GameObject newTool = Instantiate(o);
            newTool.transform.position = transform.position + new Vector3(1, -2, 0);
            newTool.gameObject.transform.parent = GameObject.FindGameObjectWithTag("Tools").transform;
        }
        else
        {
            noResourcesSignal.Raise();
        }
    }
  /*  public void updateToolsInMenu()
    {
        foreach (Transform child in content.GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }
        foreach (GameObject o in gameObjectsSystem.Tools)
        {
            GameObject nPos = Instantiate(emptyMenuOption);
            nPos.transform.FindChild("Sprite").GetComponent<SpriteRenderer>().sprite = o.transform.GetComponent<SpriteRenderer>().sprite;
            nPos.transform.FindChild("Text").GetComponent<Text>().text = o.transform.GetComponent<Tool>().Item.name;
            nPos.transform.parent = content;
            
            // o.transform.parent = content;
        }
    }*/
    public int GetStoneCost()
    {
        return (int)(-5 * (1 - (blacksmithStats.decreseCosts.Value / 100)));
    }
    public int GetWoodCost()
    {
        return (int)(-10 * (1 - (blacksmithStats.decreseCosts.Value / 100)));
    }
    public void DayMode()
    {
        transform.Find("Window").gameObject.SetActive(false);
    }
    public void NightMode()
    {
        transform.Find("Window").gameObject.SetActive(true);
    }
}
