using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
  //  public Image[] hearts;
  //  public Sprite fullHeart;
  //  public Sprite partHeart;
  //  public Sprite emptyHeart;
  //  public FloatValue heartContainers;
    public PlayerHealth playerCurrentHealth;
    public PlayerStatistics playerStatistics;
    public Slider slider;
    public Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }
    public void InitHearts()
    {
     
        slider.maxValue = playerStatistics.health.Value;
        slider.value = playerCurrentHealth.currentHealth;
        healthText.text = slider.value + "/" + slider.maxValue;
    }
    public void UpdateHearts()
    {
        InitHearts();
        slider.value = playerCurrentHealth.currentHealth;

    }
}
