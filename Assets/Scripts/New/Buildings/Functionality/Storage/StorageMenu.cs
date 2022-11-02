using UnityEngine;
using UnityEngine.UI;

public class StorageMenu : MonoBehaviour
{
    public Storage storage;
    public Text sellStone;
    public Text buyStone;
    public Text sellWood;
    public Text buyWood;
    // GameObjectsSystem gameObjectsSystem;

    // Start is called before the first frame update
    void Start()
    {
        // gameObjectsSystem = GameObject.FindGameObjectWithTag("GameObjectsSystem").GetComponent<GameObjectsSystem>();
    }
    private void OnEnable()
    {
        sellStone.text =  storage.StoneValue() + " G for 30 S";
        sellWood.text = storage.StoneValue() + " G for 50 W";
        buyStone.text = storage.GoldStonePrice() + " S for 2 G";
        buyWood.text = storage.GoldWoodPrice() + " S for 1 G";
    }

}
