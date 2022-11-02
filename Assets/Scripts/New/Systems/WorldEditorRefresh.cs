using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldEditorRefresh : MonoBehaviour
{
    public GameObject currentGrid;
    public GameObject newGrid;
    private Transform gridParent;
    private Transform gridPos;
    public void RefreshGrid()
    {
        gridParent = currentGrid.transform.parent;
        gridPos = currentGrid.transform;
        Destroy(currentGrid.gameObject);
        currentGrid = Instantiate(newGrid, gridParent);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
