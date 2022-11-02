using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class DayNightSystem : MonoBehaviour
{
    public GameObject globalLights;
  
    float colorMix=0;
    bool coIsRunning = false;
    public  bool isDay = true;
    public int dayTime;
    public int nightTime;
    public Signal2 nightSignal;
    public Signal2 daySignal;
    public int currentDay = 0;

    void Update()
    {
        if (!coIsRunning)
        {
            if (isDay)
            {
                StartCoroutine(Day(nightTime));
                isDay = !isDay;
            }
            else
            {
                StartCoroutine(Night(dayTime));
                isDay = !isDay;
            }
        }

    }
    void Start()
    {
        globalLights.GetComponent<Light2D>().color = Color.white;
        globalLights.GetComponent<Light2D>().intensity = 1;
    }
    IEnumerator Night(float nightTime)
    {
        coIsRunning = true;
        
        colorMix = 0;
        while (globalLights.GetComponent<Light2D>().intensity >= 0.7)
        {
            if (globalLights.GetComponent<Light2D>().intensity <= 0.8)
            {
                colorMix += 0.00001f;
            }
            
            if (colorMix > 1)
                colorMix = 1;
            globalLights.GetComponent<Light2D>().color = (1 - colorMix) * globalLights.GetComponent<Light2D>().color + colorMix * Color.yellow;
            globalLights.GetComponent<Light2D>().intensity -= 0.0002f;
            yield return null;
        }
        colorMix = 0;
        while (globalLights.GetComponent<Light2D>().intensity >= 0.5)
        {
     
                colorMix += 0.000003f;
   
            if (colorMix > 1)
                colorMix = 1;
            globalLights.GetComponent<Light2D>().color = (1 - colorMix) * globalLights.GetComponent<Light2D>().color + colorMix * Color.red;
            globalLights.GetComponent<Light2D>().intensity -= 0.0002f;
            yield return null;
        }
        nightSignal.Raise();
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("PowerUp"))
        {
            Destroy(item);
        }
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("NightLight"))
        {
            o.GetComponent<Light2D>().enabled= true;
        }

        while (globalLights.GetComponent<Light2D>().intensity>=0.2)
        {
            
                colorMix += 0.000006f;
            
            if (colorMix > 1)
                colorMix = 1;
            globalLights.GetComponent<Light2D>().color = (1 - colorMix) * globalLights.GetComponent<Light2D>().color + colorMix * Color.gray;
            globalLights.GetComponent<Light2D>().intensity -= 0.0002f;
            yield return null;
        }



        globalLights.GetComponent<Light2D>().color = Color.gray;
        
        yield return new WaitForSeconds(nightTime);
        coIsRunning = false;



    }
    IEnumerator Day(float dayTime)
    {
        coIsRunning = true;
       
        currentDay++;
        while (globalLights.GetComponent<Light2D>().intensity <= 0.6)
        {
            if (globalLights.GetComponent<Light2D>().intensity >= 0.4)
            {
                colorMix += 0.0006f;
            }
            if (colorMix > 1)
                colorMix = 1;
            globalLights.GetComponent<Light2D>().color = (1 - colorMix) * Color.grey + colorMix * Color.red;
            globalLights.GetComponent<Light2D>().intensity += 0.0002f;
            yield return  null;
        }
        if (currentDay != 0)
        {
            daySignal.Raise();
        }
        colorMix = 0;
        while (globalLights.GetComponent<Light2D>().intensity <= 0.7)
        {
            if (globalLights.GetComponent<Light2D>().intensity >= 0.6)
            {
                colorMix += 0.000012f;
            }
            if (colorMix > 1)
                colorMix = 1;
            globalLights.GetComponent<Light2D>().color = (1 - colorMix) * globalLights.GetComponent<Light2D>().color + colorMix * Color.yellow;
            globalLights.GetComponent<Light2D>().intensity += 0.0001f;
            yield return null;
        }
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("NightLight"))
        {
            o.GetComponent<Light2D>().enabled = false;
        }
        colorMix = 0;
        while (globalLights.GetComponent<Light2D>().intensity <= 1)
        {
            if (globalLights.GetComponent<Light2D>().intensity >= 0.7)
            {
                colorMix += 0.001f;
            }
            if (colorMix > 1)
                colorMix = 1;
            globalLights.GetComponent<Light2D>().color = (1 - colorMix) * globalLights.GetComponent<Light2D>().color + colorMix * Color.white;
            globalLights.GetComponent<Light2D>().intensity += 0.0001f;
            yield return null;
        }
        globalLights.GetComponent<Light2D>().color = Color.white;
        
        yield return new WaitForSeconds(dayTime);

        coIsRunning = false;

    }
  
  /*  IEnumerator DayCome(float time)
    {

    }
    IEnumerator NightCome(float time)
    {

    }*/
}
