using UnityEngine;

public class Building : MonoBehaviour
{
    
    //public int maxDurablity;
    public BuildingStatiscics buildingStatiscics;
    int durability;
    public bool isConstructed = false;
    string endTag;
    //public Signal2 update;
    void Start()
    {
        durability = 0;
        endTag = gameObject.tag;
        gameObject.tag = "ToBuild";
        //update.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        if (durability >= buildingStatiscics.constructionTime.Value||isConstructed)
        {
            isConstructed = true;
            gameObject.tag = endTag;
            gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.white;
           if(gameObject.GetComponent<BuildingFunction>()!=null)
           // gameObject.GetComponent<BuildingFunction>().enabled = true;
            gameObject.GetComponent<Building>().enabled = false;
            
        }
    }
    public void Construct(int power)
    {
        durability+=power;
        if (durability > buildingStatiscics.constructionTime.Value)
        {
            durability = (int)buildingStatiscics.constructionTime.Value;
        }

    }
}
