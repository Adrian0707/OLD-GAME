using UnityEngine;

public class Pedestal : MonoBehaviour
{
    public PedestalStats pedestalStats;
    public string deffensiveBuildings;
    public string offensiveBuildings;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach(GameObject g in GameObject.FindGameObjectsWithTag(deffensiveBuildings))
            {

                try
                {
                    g.transform.Find("Active").gameObject.SetActive(true);
                }
                catch (System.Exception)
                {

                    
                }
                     
            }
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(offensiveBuildings))
            {
                try
                {
                    g.transform.Find("Active").gameObject.SetActive(true);
                }
                catch (System.Exception)
                {

                    
                }
                  
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(deffensiveBuildings))
            {
                g.transform.Find("Active").gameObject.SetActive(false);
            }
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(offensiveBuildings))
            {
                g.transform.Find("Active").gameObject.SetActive(false);
            }
        }
    }
}
