using System.Collections.Generic;
using UnityEngine;

public class GameObjectsSystem : MonoBehaviour
{
    public List<GameObject> Tools;
    public List<GameObject> Buildings;
    public List<GameObject> Powers;
    public List<GameObject> DropItems;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public PowerUp FindDropItemByName(string name)
    {
        foreach (GameObject item in DropItems)
        {
            if(item.name == name)
            {
                return item.GetComponent<PowerUp>();
            }
        }
        return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
