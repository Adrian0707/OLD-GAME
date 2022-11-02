using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public Signal2 upadeGui;
    private bool isPaused;
    public GameObject pausePanel;
   // public GameObject inventoryPanel;
    public string mainMenu;
    public bool usingPausePanel;
    public GameObject[] panels;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        //pausePanel.SetActive(false);
        //inventoryPanel.SetActive(false);
        usingPausePanel = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("pause"))
        {
            ChangePause();
        }  
        
    }
    public void ChangePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            usingPausePanel = true;
           /* inventoryPanel.SetActive(true);
            Time.timeScale = 0f;
            usingPausePanel = false;*/
        }
        else
        {
            foreach (GameObject panel in panels)
            {
                panel.SetActive(false);
            }
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            upadeGui.Raise();

        }
    }
     public void QuitToMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
    public void SwitchPanel(int i)
    {
     
            //pausePanel.SetActive(true);
            foreach (GameObject panel in panels)
            {
                panel.SetActive(false);
            }
            panels[i].SetActive(true);
           // pausePanel.SetActive(false);
        
    }
    public void ClosePanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        pausePanel.SetActive(true);
    }
    public void OpenManageEnemy()
    {
        this.transform.Find("ManageEnemies").gameObject.SetActive(true);
    }
}
