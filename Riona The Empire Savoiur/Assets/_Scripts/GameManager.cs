//----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   GameManager.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/04/07
///   Description       : A game manager
///   Revision History  : 7th ed. When player loads into 2nd level, makes sure that the flames in the tutorial level stay inactive
///----------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public enum Level
{
	Tutorial,
	MainLevel
}

public class GameManager : MonoBehaviour
{
	public Transform rionaSpawnPoint;
	public Vector3 RionaPosition;
	public SceneController sceneController;
	// change to singleton if required
	// use lazy singleton for now
	public static GameManager _instance;
	public List<EnemySave> enemies;
	public TextMeshProUGUI TMP_ObjectiveText;
	public TextMeshProUGUI ObjectiveDescription;
	public List<RepresentsItem> items;
	public bool levelIsLoading;
	[Header("Objective Text")]
	public string tutorialObjective;
	public string levelObjective;

	[Header("Current Level")]
	public Level currentLevel;

	[Header("List of Level Triggers")] 
	public List<TriggerToNextScene> triggerList;

    [Header("Player Reference")] 
    public GameObject Player;

	[Header("TutorialLevelFlames")]
	public List<GameObject> TutorialFlames;
	public List<GameObject> TutorialSpikes;

    public static GameManager GetInstance()
	{
		return _instance;
	}

	private void Awake()
	{
		_instance = this;
		Data.ResetGame();

		// Set current level
		ObjectiveDescription.text = "Gather all the spellbooks";
		currentLevel = Level.Tutorial;
		tutorialObjective =   Data.spellBooksCount.ToString()+ " Spellbooks remaining in the level";
		levelObjective =   Data.enemyCount.ToString() +" Enemies remaining in the level" ;
		
		TMP_ObjectiveText.text = tutorialObjective;

		// Get all triggers of the scene
		GameObject[] tempGameObjectsArray = GameObject.FindGameObjectsWithTag("LevelTrigger");
		foreach (var trigger in tempGameObjectsArray)
		{
			triggerList.Add(trigger.GetComponent<TriggerToNextScene>());
		}
	}

	public void loadData()
	{
		if(currentLevel == Level.Tutorial)
		{
			ObjectiveDescription.text= "Gather all the spellbooks";
			TMP_ObjectiveText.text = Data.spellBooksCount.ToString() + " Spellbooks remaining in the level";
		}
		else if(currentLevel == Level.MainLevel)
		{
			ObjectiveDescription.text = "Kill all enemies";
			TMP_ObjectiveText.text = Data.enemyCount.ToString() + " Enemies remaining in the level";
			DeactivateTutorialFlames();
			DeactivateTutorialSpikes();
		}
	}

	public void DeactivateTutorialFlames()
    {
        foreach (GameObject flames in TutorialFlames)
        {
            flames.SetActive(false);
        }
    }

	public void DeactivateTutorialSpikes()
    {
		foreach (GameObject spikes in TutorialSpikes)
        {
            spikes.SetActive(false);
        }
    }


	// Start is called before the first frame update
	void Start()
	{
		RionaPosition = rionaSpawnPoint.position;
		int id = 0;
		enemies = FindObjectsOfType<EnemySave>().ToList();
		items = FindObjectsOfType<RepresentsItem>().ToList();

		foreach (var item in items)
		{
			item.m_id = (id++).ToString();
		}

		id = 0;

		foreach (var enemy in enemies)
		{
			enemy.m_id = (id++).ToString();
		}

    }

 

	public void SetCurrentCheckPoint(Vector3 position)
	{
		//rionaSpawnPoint.position = position;
		RionaPosition = position;
	}

	public void EnemyManager_KillEnemy(int score)
	{

		if (currentLevel == Level.MainLevel)
		{
			//Debug.Log("Enemies remaining" + Data.enemyCount);
			Data.enemyCount -= 1;
			TMP_ObjectiveText.text = "Enemies to kill: " + Data.enemyCount.ToString();

			if (Data.enemyCount <= 0)
			{
				Data.isWon = true;
                // If you acquire all the spellbooks
               
               ObjectiveDescription.text = "Proceed to the end point";
               tutorialObjective = "";
               TMP_ObjectiveText.text = tutorialObjective;

               foreach (var trigger in triggerList)
               {
                   if (trigger.whichLevelTriggerIsThis == currentLevel)
                   {
						//Debug.Log("blah: " + currentLevel);
                   }
               }
                
            }
		}
		
		
		Data.score += score;
		
	}

	/// <summary>
	/// Item pick up function updated
	/// </summary>
	/// <param name="score"></param>
	public void ItemPickUp(int score)
	{
		Data.score += score;

    }

    // Fix for potion pick up updating the objectives
    public void SpellBookPickUp()
    {
        if (currentLevel == Level.Tutorial)
        {
            Data.spellBooksCount--;
            tutorialObjective = "Spellbooks remaining: " + Data.spellBooksCount.ToString();
            TMP_ObjectiveText.text = tutorialObjective;

            // If you acquire all the spellbooks
            if (Data.spellBooksCount <= 0)
            {
                ObjectiveDescription.text = "Proceed to the end point";
                tutorialObjective = "";
                TMP_ObjectiveText.text = tutorialObjective;

                foreach (var trigger in triggerList)
                {
                    if (trigger.whichLevelTriggerIsThis == currentLevel)
                    {
                        trigger.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                    }
                }
            }
        }
	}


	public void SetTime(float time)
	{
		Data.LevelTimer = time;
		if(Data.LevelTimer<=0)
		{
			//Debug.Log("time ran out");
			sceneController.ChangeToEndScene();
		}
	}
}
