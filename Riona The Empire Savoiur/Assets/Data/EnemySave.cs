using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemySave : MonoBehaviour,ISaveable
{
    
    public string m_id;
    private Paladin_Behaviour paladinBehaviour;
    private FlyingEnemy_Behaviour flyingEnemy;
    private Slider Enemy_HealthBar_ScreenSpace_Slider;
    private bool isPaladin;
    private void Start()
	{
        if (GetComponent<Paladin_Behaviour>())
        {
            paladinBehaviour = GetComponent<Paladin_Behaviour>();
            isPaladin = true;
        }
        else if (GetComponent<FlyingEnemy_Behaviour>())
        {
            flyingEnemy = GetComponent<FlyingEnemy_Behaviour>();
            isPaladin = false;
        }
        Enemy_HealthBar_ScreenSpace_Slider = GetComponentInChildren<Slider>(); //GetComponent<Slider>();

    }
    public void PopulateSaveData(SaveData a_SaveData)
    {
        SaveData.EnemyData enemy = new SaveData.EnemyData();
        enemy.m_id = m_id;
        if(isPaladin)
        enemy.m_health = paladinBehaviour.Enemy_currentHealth;
        else
            enemy.m_health = flyingEnemy.Enemy_currentHealth;
        enemy.posX = transform.position.x;
        enemy.posY = transform.position.y;
        enemy.posZ = transform.position.z;

        enemy.Rotx = this.transform.rotation.x;
        enemy.Roty = this.transform.rotation.y;
        enemy.Rotz = this.transform.rotation.z;
        enemy.Rotw = this.transform.rotation.w;

        a_SaveData.enemies.Add(enemy);
    }

    public void LoadFromSaveData(SaveData a_SaveData)
    {
        bool found = false;
        foreach (var enemy in a_SaveData.enemies)
        {
            if (m_id == enemy.m_id)
            {
                this.transform.position = new Vector3(enemy.posX, enemy.posY, enemy.posZ);
                this.transform.rotation.Set(enemy.Rotx, enemy.Roty, enemy.Rotz, enemy.Rotw);
                if (isPaladin)
                {
                    paladinBehaviour.Enemy_currentHealth = enemy.m_health;
                    Enemy_HealthBar_ScreenSpace_Slider.value = paladinBehaviour.Enemy_currentHealth;
                }
                else
                {
                    flyingEnemy.Enemy_currentHealth = enemy.m_health;
                    Enemy_HealthBar_ScreenSpace_Slider.value = flyingEnemy.Enemy_currentHealth;
                }
                found = true;
                break;
            }
        }
        if(!found)
		{
            Destroy(gameObject);
		}
   
        if (isPaladin && paladinBehaviour.Enemy_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        else if(!isPaladin && flyingEnemy.Enemy_currentHealth <= 0)
		{
            Destroy(gameObject);
        }
        


    }
}
