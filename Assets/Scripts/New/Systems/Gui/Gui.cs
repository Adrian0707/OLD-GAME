using UnityEngine;
using TMPro;

public class Gui : MonoBehaviour
{

    [SerializeField]public int wood;
    [SerializeField] public int stone;
    [SerializeField] public int gold;
    [SerializeField] public int coin;
    public GameObject woodHolder;
    public GameObject stoneHolder;
    public GameObject goldHolder;
    public GameObject coinHolder;
    public GameObject heroCanvas;
    public GameObject strategyCanvas;
    new public GameObject camera;

    private void Start()
    {
       // wood = 0;
        woodHolder.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = wood.ToString();
       // gold = 0;
        goldHolder.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = gold.ToString();
        //stone = 0;
        stoneHolder.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = stone.ToString();
       // coin = 0;
        coinHolder.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = coin.ToString();
    }
    public void ModifyWood(int amount)
    {
        wood += amount;
        woodHolder.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = wood.ToString();
    }
    public void ModifyGold(int amount)
    {
        gold += amount;
        goldHolder.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = gold.ToString();
    }
    public void ModifyStone(int amount)
    {
        stone += amount;
        stoneHolder.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = stone.ToString();
    }
    public void ModifyCoin(int amount)
    {
        coin += amount;
        coinHolder.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = coin.ToString();
        SaveSystem.SaveCoins();
    }
    public void GoToHeroMode()
    {
        strategyCanvas.SetActive(false);
        heroCanvas.SetActive(true);
        camera.GetComponent<CameraMovment>().enabled = true;
        camera.GetComponent<CameraTouch>().enabled = false;
        camera.GetComponent<Camera>().orthographicSize = 6;
    }
    public void GoToStrategyMode()
    { 
        heroCanvas.SetActive(false);
        strategyCanvas.SetActive(true);
       
        camera.GetComponent<CameraMovment>().enabled = false;
        camera.GetComponent<CameraTouch>().enabled = true;
        camera.GetComponent<Camera>().orthographicSize = 14;
    }
    public void CloseAllMenus()
    {
        foreach(GameObject o in GameObject.FindGameObjectsWithTag("ObjectMenu"))
        {
            o.SetActive(false);
        }

    }
}
