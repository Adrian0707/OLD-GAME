using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public GameObject[] Objects;
    public GameObject emptyMenuOption;
    public Transform content;

    private void OnMouseDown()
    {
        if (GetComponent<Building>().isConstructed&&GameObject.FindGameObjectWithTag("StrategyGUI")!=null)
        {
            if (GetComponent<Fireplace>() == null || GetComponent<Fireplace>().isActiveAndEnabled)
            {
                if (!gameObject.transform.Find("Menu").gameObject.active)
                {
                    gameObject.transform.Find("Menu").gameObject.SetActive(true);
                    //  UpdateMenu();
                }
                else
                    gameObject.transform.Find("Menu").gameObject.SetActive(false);
            }
        }
    }
    void UpdateMenu()
    {
       // content = gameObject.transform.Find("Content");
        foreach (GameObject o in Objects)
        {
            Transform nPos = Instantiate(emptyMenuOption).transform;
            nPos.parent = content;
            nPos.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = o.transform.GetComponent<SpriteRenderer>().sprite;
            nPos.transform.Find("Text").GetComponent<Text>().text = o.transform.GetComponent<Tool>().Item.name;
               // o.transform.parent = content;
        }
    }
   /* private void OnMouseExit()
    {
        gameObject.transform.Find("Menu").gameObject.SetActive(false);
    }*/
}
