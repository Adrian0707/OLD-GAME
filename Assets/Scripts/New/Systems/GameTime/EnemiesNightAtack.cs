using System.Collections.Generic;
using UnityEngine;
using System.Data;
public class EnemiesNightAtack : MonoBehaviour
{
   // Expression e = new Expression("2 + 3 * 5");
    public string difficultyEquation;
    static DataTable dt = new DataTable();
    public int Day=1;
    bool correctEquation;
    public EnemiesSystem enemiesSystem;
    public Fireplace fireplace;
    public int Danger;
    public Transform enemiesObj;
    
    public int CurrentDificulty()
    {
        string difficultyEquationMod;
        difficultyEquationMod = difficultyEquation.Replace("DAY", Day.ToString());
        return (int)dt.Compute(difficultyEquationMod, "");
        
    }
    private void Start()
    {
        string difficultyEquationMod;
        difficultyEquationMod = difficultyEquation.Replace("DAY", Day.ToString());
        correctEquation = true;
        try
        {
            dt.Compute(difficultyEquationMod, "");
        }
        catch (System.SystemException)
        {
            correctEquation = false;
        }
        if (correctEquation)
        {

            //print(CurrentDificulty() + "");
        }
        else { print("Bad equasieon"); }
    }
    public bool IfEquasionIsCorrect(string equation)
    {
        if (equation.Contains("DAY"))
        {
            string difficultyEquationMod;

            difficultyEquationMod = equation.Replace("DAY", Day.ToString());

            correctEquation = true;
            try
            {
                dt.Compute(difficultyEquationMod, "");
            }
            catch (System.SystemException)
            {
                correctEquation = false;
            }
        }
        else
        {
            correctEquation = false;
        }
        return correctEquation;
    }
    public void NightAttack(int Danger)
    {
        Danger *= CurrentDificulty();
        print(Danger);
        Day++;
        List<GameObject> enemies = enemiesSystem.nightEnemies;
        enemies.AddRange(enemiesSystem.playerNightEnemiesInGame);
        for (int i = 0; i < 100; i++)
        {
            int enem = Random.Range(0, enemies.Count);
            if (enemies[enem].GetComponent<Enemy>().enemyStatistics.power <= Danger*0.2)
            {
                Danger -= enemies[enem].GetComponent<Enemy>().enemyStatistics.power;
                Vector3 spawn ;
                if (Random.Range(0, 2) == 1)
                {
                    if (Random.Range(0, 2) == 1)
                    {
                        spawn = fireplace.transform.position + new Vector3(-Random.Range(30, 100), Random.Range(30, 100));
                    
                    }
                    else
                    {
                        spawn = fireplace.transform.position + new Vector3(-Random.Range(30, 100), -Random.Range(30, 100));

                    }
                }
                else
                {
                    if (Random.Range(0, 2) == 1)
                    {
                        spawn = fireplace.transform.position + new Vector3(Random.Range(30, 100), Random.Range(30, 100));
                      
                    }
                    else
                    {
                        spawn = fireplace.transform.position + new Vector3(Random.Range(30, 100), -Random.Range(30, 100));
                       
                    }
                }
                GameObject gameObject = GameObject.Instantiate(enemies[enem], spawn, Quaternion.identity);
                gameObject.transform.SetParent(enemiesObj);
                gameObject.SetActive(true);
            }
        }
    }

}
