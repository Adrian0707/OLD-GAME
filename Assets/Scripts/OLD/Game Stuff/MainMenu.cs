using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text days;
    public Text gold;
    
    void Start()
    {
        days.text = "Days survived " + SaveSystem.LoadDays();
        gold.text = "Gold: " + SaveSystem.GetLoadCoins();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DeleteSaves()
    {
        string path = Application.persistentDataPath + "/Upgrades.inf";
        try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        path = Application.persistentDataPath + "/Coins.inf";
        try
        {
            File.Delete(path);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        path = Application.persistentDataPath + "/Days.inf";
        try
        {
            File.Delete(path);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        days.text = "Days survived " + SaveSystem.LoadDays();
        gold.text = "Gold: " + SaveSystem.GetLoadCoins();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("MapGenerationSettings");
    }
    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
