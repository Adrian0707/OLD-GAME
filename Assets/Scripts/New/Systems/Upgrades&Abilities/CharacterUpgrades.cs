using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterUpgrades : MonoBehaviour
{
    public UpgradeSystem upgradeSystem;
    public GameObject emptyUpgrade;
    public GameObject content;
    public TextMeshProUGUI description;
    public TextMeshProUGUI buttonText;
    public Button descButton;
    //public Text price;
    //public GameObject priceElem;
    void Start()
    {
        //upgradeSystem = GameObject.FindGameObjectWithTag("UpagradeSystem").GetComponent<UpgradeSystem>();
       
    }


    private void OnEnable()
    {
        foreach(Transform child in content.transform)
        {
           Destroy(child.gameObject);
        }
        GameObject empty;
        foreach(Upgrade up in upgradeSystem.charactersUpgrades)
        {
            empty =Instantiate(emptyUpgrade);
            empty.transform.SetParent(content.transform,false);
            empty.transform.Find("Main image").GetComponent<Image>().sprite = up.sprite;
            empty.transform.Find("Secound image").GetComponent<Image>().sprite = up.sprite2;
            empty.transform.Find("Name").transform.Find("Text").GetComponent<Text>().text = up.name;
            empty.GetComponent<Button>().onClick.AddListener(() => desctiptionSetAndButton(up));
            empty.GetComponent<Image>().color = setColor(up);
            empty.transform.Find("Cost").transform.Find("Text").GetComponent<Text>().text = up.cost.ToString();
            if (up.bought)
            {
                empty.transform.Find("Cost").gameObject.SetActive(false);
            }
        }

    }
    public void desctiptionSetAndButton(Upgrade up)
    {
        descButton.onClick.RemoveAllListeners();
        descButton.onClick.AddListener(() => empty());
     
        description.SetText(up.description);
        descButton.enabled = true;
        if (!up.bought)
        {
            if (up is UpPlayer)
            {
                buttonText.SetText("Buy");
                descButton.onClick.AddListener(()=>up.buy()); 
            }
        }
        else
        {
            if (!up.activated)
            {
                if (up is UpPlayer)
                {
                    buttonText.SetText("Activate");
                    descButton.onClick.AddListener(() => up.Equip());
                }
            }
            else
            {
                    if (up is UpPlayer)
                    {
                        buttonText.SetText("Deactivate");
                        descButton.onClick.AddListener(() => up.Unequip());
                    }
           
            }
        }
        descButton.onClick.AddListener(() => OnEnable());
        descButton.onClick.AddListener(() => SaveSystem.SaveUpgrades());
       // descButton.onClick.AddListener(() => SaveSystem.LoadUpgrades());
    }
    public void empty()
    {
        description.SetText("");
        buttonText.SetText("");
        descButton.onClick.RemoveAllListeners();
        descButton.onClick.AddListener(() => OnEnable());
        descButton.enabled = false;
    }
    public Color setColor(Upgrade up)
    {
        
        if (!up.bought)
        {
            return Color.white;
        }
        else
        {
            if (!up.activated)
            {
              //  if (up is UpPlayerHealth)
                {
                    return Color.grey;
                }
            }
            else
            {
              //  if (up is UpPlayerHealth)
                {
                    return Color.green;
                }

            }
        }
    }

}
