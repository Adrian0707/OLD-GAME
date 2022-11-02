using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Slider))]
public class SetValueSlider : MonoBehaviour
{
   // public Slider slider;
    public void SliderShowValue(Text text)
    {
        text.text =this.GetComponent<Slider>().value+"";
    }

}
