using UnityEngine;

public class OnClickOpenClose : MonoBehaviour
{
    // Start is called before the first frame update
  public void click()
    {
        if (gameObject.activeSelf) gameObject.SetActive(false);
        else gameObject.SetActive(true);
    }
}
