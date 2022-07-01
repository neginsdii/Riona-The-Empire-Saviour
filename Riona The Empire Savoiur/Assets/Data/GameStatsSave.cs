using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatsSave : MonoBehaviour,ISaveable
{
	public void LoadFromSaveData(SaveData a_SaveData)
	{
		Data.score = a_SaveData.gameStats.score;
		Data.enemyCount = a_SaveData.gameStats.enemyCount;
		Data.spellBooksCount = a_SaveData.gameStats.numOfSpellBooks;
		GameManager._instance.currentLevel =(Level) a_SaveData.gameStats.CurrentLevel;
		GameManager._instance.loadData();
	}

	public void PopulateSaveData(SaveData a_SaveData)
	{
		SaveData.GameStats gameStats = new SaveData.GameStats();
		gameStats.score = Data.score;
		gameStats.enemyCount = Data.enemyCount;
		gameStats.numOfSpellBooks = Data.spellBooksCount;
		gameStats.CurrentLevel =(int) GameManager._instance.currentLevel;
		a_SaveData.gameStats = gameStats;
	}

	
}
