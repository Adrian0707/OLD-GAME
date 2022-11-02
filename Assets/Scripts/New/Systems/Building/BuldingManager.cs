using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuldingManager : MonoBehaviour
{
    public GameObject buildingsContainer;
    public GameObject button;
    public GameObjectsSystem gameObjectsSystem;
    public BuildingPlacement buldingPlacement;
    public GameObject panelToClose; 
    //private int i;
    // Start is called before the first frame update
    void Start()
    {
       // buldingPlacement = GetComponent<BuildingPlacement>();
        //gameObjectsSystem = GameObject.FindGameObjectWithTag("GameObjectsSystem").GetComponent<GameObjectsSystem>();
        OnUpdateGUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnUpdateGUI()
    {
        //buildingsContainer.GetComponent<GridLayoutGroup>().cellSize =new Vector2(Screen.width/ 8, Screen.height / 5);
        
        foreach (Transform o in buildingsContainer.transform)
        {
            Destroy(o.gameObject);
        }
       // for(int i = 0; i < gameObjectsSystem.Buildings.Length; i++)
       foreach(GameObject o in gameObjectsSystem.Buildings)
        {
            GameObject newOpt = Instantiate(button);
            if(o.GetComponent<BuildingReqirements>()!=null)
            newOpt.transform.SetParent(buildingsContainer.transform,false);
            newOpt.gameObject.GetComponent<Button>().onClick.AddListener(delegate(){ buldingPlacement.SetItem(o); });
            newOpt.gameObject.GetComponent<Button>().onClick.AddListener(closePanel);
            newOpt.gameObject.transform.Find("BuildingImage").GetComponent<Image>().sprite = o.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite;
            BuildingReqirements req = o.GetComponent<BuildingReqirements>();
            newOpt.gameObject.transform.Find("NameText").GetComponent<Text>().text = o.name;
            newOpt.gameObject.transform.Find("DescriptionText").GetComponent<Text>().text = req.buildingStatiscics.Description;
            newOpt.gameObject.transform.Find("WoodInfo").Find("Text").GetComponent<TextMeshProUGUI>().text = req.buildingStatiscics.woodReq.Value+"";
            newOpt.gameObject.transform.Find("GoldInfo").Find("Text").GetComponent<TextMeshProUGUI>().text = req.buildingStatiscics.goldReq.Value + "";
            newOpt.gameObject.transform.Find("StoneInfo").Find("Text").GetComponent<TextMeshProUGUI>().text = req.buildingStatiscics.stoneReq.Value + "";
            newOpt.gameObject.transform.Find("ConstructionTimeInfo").Find("Text").GetComponent<TextMeshProUGUI>().text = req.buildingStatiscics.constructionTime.Value + "";

           /* newOpt.transform.Find("Text").GetComponent<Text>().text = "" + o.name + "\n " + req.buildingStatiscics.goldReq.Value
                + "g+" + req.buildingStatiscics.woodReq.Value + "w+" + req.buildingStatiscics.stoneReq.Value + "s";*/
        }
    }
    public void closePanel()
    {
        panelToClose.SetActive(false);
    }
}
