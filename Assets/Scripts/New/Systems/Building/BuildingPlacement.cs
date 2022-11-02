using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private BuildingPlacable placable;
    [HideInInspector] public Transform currentBuilding;
    [HideInInspector]public bool hasPlaced;
    List<BoxCollider2D> bounds;
    public LayerMask buildingMask;
    string endTag;
    public Signal2 upadePath;
    public Signal2 noResources;
    public Gui gui;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 m = Input.mousePosition;
        // m = new Vector3(m.x, m.y, transform.position.y);
        Vector3 p = Camera.main.ScreenToWorldPoint(m);
        if (currentBuilding != null && !hasPlaced)
        {
           // Debug.LogError(inBounds());

            currentBuilding.position = new Vector3(p.x, p.y, -1);
            currentBuilding.transform.Find("Sprite").GetComponent<SpriteRenderer>().sortingOrder = -(int)currentBuilding.transform.position.y+2 ;

            if (!IsLegalPosition() || !IsInBounds() )
            {
                currentBuilding.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                currentBuilding.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.green;
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (IsLegalPosition()&& IsInBounds())
                {
                    currentBuilding.tag = endTag;
                    currentBuilding.GetComponentInChildren<SpriteRenderer>().sortingOrder = -(int)currentBuilding.transform.position.y;
                    currentBuilding.GetComponent<Building>().enabled = true;
                    if (currentBuilding.GetComponent<BoxCollider2D>())
                    {
                        currentBuilding.GetComponent<BoxCollider2D>().enabled = true;
                    }
                    else if (currentBuilding.GetComponent<CapsuleCollider2D>())
                    {
                        currentBuilding.GetComponent<CapsuleCollider2D>().enabled = true;
                    }
                    else if (currentBuilding.GetComponent<CircleCollider2D>())
                    {
                        currentBuilding.GetComponent<CircleCollider2D>().enabled = true;
                    }
                        // currentBuilding.FindChild("Sprite").GetComponent<SpriteRenderer>().color = Color.white;
                        hasPlaced = true;
                    //currentBuilding = null;
                    upadePath.Raise();
                }
            }
        }
    }
        
    bool  IsLegalPosition()
    {
        

        if (placable.collider2s.Count > 0&& currentBuilding.position.y <= Screen.height / 10)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    bool IsInBounds()
    {
        Fireplace fireplace=GameObject.FindGameObjectWithTag("Fireplace").GetComponent<Fireplace>();
        return fireplace.IsInBounds(currentBuilding);
    }
    public void SetItem(GameObject o)
    {
        if (o.GetComponent<BuildingReqirements>().canBeBuild(gui.gold, gui.stone, gui.wood))
        {
            gui.ModifyGold(-(int)o.GetComponent<BuildingReqirements>().buildingStatiscics.goldReq.Value);
            gui.ModifyStone(-(int)o.GetComponent<BuildingReqirements>().buildingStatiscics.stoneReq.Value);
            gui.ModifyWood(-(int)o.GetComponent<BuildingReqirements>().buildingStatiscics.woodReq.Value);
            hasPlaced = false;
          //  Debug.Log(o.name);
            currentBuilding = ((GameObject)Instantiate(o)).transform;
            currentBuilding.parent = GameObject.FindGameObjectWithTag("Buildings").transform;
            endTag = currentBuilding.tag;
            currentBuilding.tag = "Untagged";
           // Debug.LogError(endTag);
          //  Debug.LogError(currentBuilding.tag);
            placable = currentBuilding.GetComponent<BuildingPlacable>();
        }
        else
            noResources.Raise();
    }
    public void PutItem(GameObject o)
    {
        if (o.GetComponent<BuildingReqirements>().canBeBuild(gui.gold, gui.stone, gui.wood))
        {
            gui.ModifyGold(-(int)o.GetComponent<BuildingReqirements>().buildingStatiscics.goldReq.Value);
            gui.ModifyStone(-(int)o.GetComponent<BuildingReqirements>().buildingStatiscics.stoneReq.Value);
            gui.ModifyWood(-(int)o.GetComponent<BuildingReqirements>().buildingStatiscics.woodReq.Value);
            hasPlaced = false;
           // Debug.Log(o.name);
            currentBuilding = o.transform;
            currentBuilding.parent = GameObject.FindGameObjectWithTag("Buildings").transform;
            endTag = currentBuilding.tag;
            //currentBuilding.tag = "Untagged";
           // Debug.LogError(endTag);
           // Debug.LogError(currentBuilding.tag);
            placable = currentBuilding.GetComponent<BuildingPlacable>();
        }
        else
            noResources.Raise();
    }
    public void ClearItem()
    {
        if (currentBuilding!=null&&!hasPlaced)
        {
            gui.ModifyGold((int)currentBuilding.GetComponent<BuildingReqirements>().buildingStatiscics.goldReq.Value);
            gui.ModifyStone((int)currentBuilding.GetComponent<BuildingReqirements>().buildingStatiscics.stoneReq.Value);
            gui.ModifyWood((int)currentBuilding.GetComponent<BuildingReqirements>().buildingStatiscics.woodReq.Value);
            Destroy(currentBuilding.gameObject);
            currentBuilding = null;
            hasPlaced = true;
        }
    }
  /* bool inBounds() { 
    
        bool inb =false;
       // Debug.LogError(GameObject.FindGameObjectsWithTag("Map"));
        foreach (GameObject a in GameObject.FindGameObjectsWithTag("Map"))
        {
         //   Debug.LogError(currentBuilding.position);
            Debug.LogError(a.name);
            if (a.GetComponent<BoxCollider2D>().bounds.Contains(currentBuilding.position))
                inb=true;
        }
        return inb;
    }
*/
}
