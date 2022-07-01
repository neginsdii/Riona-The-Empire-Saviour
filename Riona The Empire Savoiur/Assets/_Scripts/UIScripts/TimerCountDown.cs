//----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   TimerCountDown.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/02/04
///   Description       : Control Movement, Jump, Camera and Animations 
///   Revision History  : 3rd ed. Reseting thetimer when loading screen is activated.
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerCountDown : MonoBehaviour,ISaveable
{
	public TextMeshProUGUI textDisplay;
	public float gameTime;
	public float level2GameTime;
	private float timer;
	private bool stopTimer;

    private int minutes = 0;
    private int seconds = 0;
    private string textTime = "";

	private void Start()
	{
		stopTimer = false;
		timer = gameTime;
	}

	private void Update()
	{
		if (GameManager.GetInstance().levelIsLoading)
			timer = level2GameTime;

			UpdateTimer();
	}

	private void UpdateTimer()
	{
		timer -= Time.deltaTime;
		minutes = Mathf.FloorToInt(timer / 60);
		seconds = Mathf.FloorToInt(timer - minutes * 60f);
		textTime = string.Format("{0:00}:{1:00}", minutes, seconds);
		GameManager._instance.SetTime(timer);
		if (timer<=0)
		{
			stopTimer = true;
			timer = 0;
		}
		if(!stopTimer)
		{
			textDisplay.SetText( textTime);
		}
	}
	public void LoadFromSaveData(SaveData a_SaveData)
	{

		timer = a_SaveData.LevelTimer;

	}

	public void PopulateSaveData(SaveData a_SaveData)
	{
		a_SaveData.LevelTimer = timer;

	}


}
