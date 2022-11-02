using UnityEngine;

public class DeactivatedTower : MonoBehaviour
{
    public Building building;
  public void day()
    {
        transform.Find("Active").gameObject.SetActive(false);
    }
  public void night()
    {
        if(building.isConstructed)
        transform.Find("Active").gameObject.SetActive(true);
    }
}
