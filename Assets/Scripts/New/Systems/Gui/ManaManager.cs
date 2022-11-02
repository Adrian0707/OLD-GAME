using UnityEngine;
using UnityEngine.UI;
public class ManaManager : MonoBehaviour
{
  //  public Image[] hearts;
  //  public Sprite fullHeart;
  //  public Sprite partHeart;
  //  public Sprite emptyHeart;
  //  public FloatValue heartContainers;
    public GenericMana playerMana;
    public PlayerStatistics playerStatistics;
    public Slider slider;
    public Text ManaText;
    // Start is called before the first frame update
    void Start()
    {
        InitMana();
    }
    public void InitMana()
    {
     
        slider.maxValue = playerStatistics.mana.Value;
        slider.value = playerMana.currentMana;
        ManaText.text = slider.value + "/" + slider.maxValue;
    }
    public void UpdateMana()
    {
        InitMana();
        slider.value = playerMana.currentMana;

    }
}
