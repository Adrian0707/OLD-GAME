using UnityEngine;
using UnityEngine.UI;

public class BlacksmithMenu : MonoBehaviour
{
    public Blacksmith blacksmith;
    public GameObject option1;
    public GameObject option2;
    public GameObject option3;
    // GameObjectsSystem gameObjectsSystem;

    // Start is called before the first frame update
    void Start()
    {
        // gameObjectsSystem = GameObject.FindGameObjectWithTag("GameObjectsSystem").GetComponent<GameObjectsSystem>();
    }
    private void OnEnable()
    {
        option1.transform.Find("Text").GetComponent<Text>().text = "Hammer " + blacksmith.GetWoodCost() + "w " + blacksmith.GetStoneCost() + "s";
        option2.transform.Find("Text").GetComponent<Text>().text = "Axe " + blacksmith.GetWoodCost() + "w " + blacksmith.GetStoneCost() + "s";
        option3.transform.Find("Text").GetComponent<Text>().text = "Pick " + blacksmith.GetWoodCost() + "w " + blacksmith.GetStoneCost()+"s";

    }

}
