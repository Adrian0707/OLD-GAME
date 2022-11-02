using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class InventorySaver : MonoBehaviour
{

    [SerializeField] private PlayerInventory myInventory;

    private void OnEnable()
    {
        myInventory.myInventory.Clear();
        LoadScriptables();
    }
    private void OnDisable()
    {

        SaveScriptables();
    }
    public void ResetSciptables()
    {
        int i = 0;
        while (File.Exists(Application.persistentDataPath + string.Format("/{0}.inv", i)))
        {
            File.Delete(Application.persistentDataPath + string.Format("/{0}.inv", i));
            i++;
        }
        
    }
   
    public void SaveScriptables()
    {
        ResetSciptables();
        for (int i = 0; i < myInventory.myInventory.Count; i++)
        {
            
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.inv", i));
            BinaryFormatter binary = new BinaryFormatter();
            var jason = JsonUtility.ToJson(myInventory.myInventory[i]);
            binary.Serialize(file, jason);
            file.Close();
        }
    }
    public void LoadScriptables()
    {
        int i = 0;
      while(File.Exists(Application.persistentDataPath + string.Format("/{0}.inv", i)))
        {
            var temp = ScriptableObject.CreateInstance<InventoryItem>();
            FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.inv", i), FileMode.Open);
            BinaryFormatter binary = new BinaryFormatter();
            JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), temp);
            file.Close();
            myInventory.myInventory.Add(temp);
            i++;
        }
        
    }
    /*private void Awake()
    {
        if (gameSave == null)
        {
            gameSave = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update*/
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
