///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   EnemyHealthBarScreenSpaceController.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/02/16
///   Description       : Added EnemyHealthBarScreenSpaceController
///   Revision History  : 1st ed. Created the EnemyHealthBar and attached the script  
///-----------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Make a class avialable anywhere into the Game
[System.Serializable]
public class EnemyHealthBarScreenSpaceController : MonoBehaviour
{
    private Slider Enemy_HealthBar_ScreenSpace_Slider;

    [Header("Enemy Health Properties")]
    [Range(0,100)]
    public int Enemy_currentHealth = 100;

    [Range(1, 100)]
    public int Enemy_maximumHealth = 100;

    public int Score = 10;

    // Start is called before the first frame update
    void Start()
    {
        Enemy_HealthBar_ScreenSpace_Slider = GetComponent<Slider>();
        Enemy_currentHealth = Enemy_maximumHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    public void TakeDamage(int damage)
    {
        Enemy_HealthBar_ScreenSpace_Slider.value -= damage;
        Enemy_currentHealth -= damage;

        if(Enemy_currentHealth < 0)
        {
            Enemy_HealthBar_ScreenSpace_Slider.value = 0;
            Enemy_currentHealth = 0;
            GameManager.GetInstance().EnemyManager_KillEnemy(Score);
        }
    }

    public void Reset()
    {
        Enemy_HealthBar_ScreenSpace_Slider.value = Enemy_maximumHealth;
        Enemy_currentHealth = Enemy_maximumHealth;
    }
}
