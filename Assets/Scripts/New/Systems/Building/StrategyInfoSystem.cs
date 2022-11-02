using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StrategyInfoSystem : MonoBehaviour
{
    public DayNightSystem dayNightSystem;
    public Image imageGameOver;
    public GameObject stratCanv;
    public GameObject heroCanv;
    //public GameObject consCanv;
  public void NoEnoguhtResources()
    {
      StartCoroutine(InfoPrint("No enought resources",1,Color.red));

    }
    public void StartGame()
    {
        StartCoroutine(InfoPrint("Choose place for the fireplace", 2, Color.white));

    }
    public void NightStart()
    {
        StartCoroutine(InfoPrint("Night is coming" , 2, Color.white));
    }
    public void DayStart()
    {
        StartCoroutine(InfoPrint("New day "+dayNightSystem.currentDay, 2, Color.white));
        SaveSystem.SaveDays(dayNightSystem.currentDay);
    }
    public void GameOver()
    {
        StartCoroutine(InfoPrintGameOver("Game Over you survived " + dayNightSystem.currentDay+" Days", Color.white));
    }
    IEnumerator InfoPrint(string text, float time,Color color)
    {
        Color t = color;
        t.a = 255;
        this.GetComponent<Text>().color = t;
        this.GetComponent<Text>().text = text;
        yield return new WaitForSeconds(time);
        t.a = 0;
        this.GetComponent<Text>().color = t;

    }
    IEnumerator InfoPrintGameOver(string text, Color color)
    {
        stratCanv.SetActive(false);
       // consCanv.SetActive(false);
        heroCanv.SetActive(false);
        Color t = color;
       // t.a = 1;
        //this.GetComponent<Text>().color = t;
       // this.GetComponent<Text>().text = text;
        
        Color t2 = imageGameOver.color;
        //t2.a = 0;
        //imageGameOver.color = t2;
        imageGameOver.gameObject.SetActive(true);
       for (int i = 0; i < 255; i++)
        {
            t2.a = i/255f;
            t.a = i / 255f;
            imageGameOver.color = t2;
            this.GetComponent<Text>().color = t;
            this.GetComponent<Text>().text = text;
            yield return new WaitForSeconds(0.02f);
           // Debug.LogError(i);
            
        }
        SaveSystem.SaveCoins();
        SaveSystem.SaveUpgrades();
       // SaveSystem.SaveDays(dayNightSystem.currentDay);
       // yield return new WaitForSeconds(10);
        SceneManager.LoadScene("StartMenu");
        // this.GetComponent<Text>().color = t;


    }

}
