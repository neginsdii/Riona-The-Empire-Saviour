///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   SaveData.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/03/09
///   Description       : Contains variables for saving and loading game stats
///   Revision History  : 1st ed. added player data, enemyData, Isavable class
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SaveData
{
	[System.Serializable]
	public struct EnemyData
	{
		public string m_id;
		public int m_health;

		public float posX;
		public float posY;
		public float posZ;

		public float Rotx;
		public float Roty;
		public float Rotz;
		public float Rotw;
	}
	[System.Serializable]
	public struct PlayerData
	{
		public float m_health;

		public float posX;
		public float posY;
		public float posZ;

		public float Rotx;
		public float Roty;
		public float Rotz;
		public float Rotw;
	}
	[System.Serializable]
	public struct GameStats
	{
		public int enemyCount;
		public int score;
		public int numOfSpellBooks;
		public int CurrentLevel;
	}

	[System.Serializable]
	public struct Item
	{
		public string m_id;
		public float posX;
		public float posY;
		public float posZ;

		public string name;
		public string descreption;
		public int type;
	}
	[System.Serializable]
	public struct InventoryItem
	{
		public string name;
		public string descreption;
		public int Quantity;
		public int type;
	}
	
	

	public GameStats gameStats;
	public float LevelTimer;
	public PlayerData player;
	public List<EnemyData> enemies = new List<EnemyData>();
	public List<Item> items = new List<Item>();
	public List<InventoryItem> InventoryItems = new List<InventoryItem>();
	public bool[] ProjectilesEnabled = new bool[3];

	//inventory

}

public interface ISaveable
{
	void PopulateSaveData(SaveData a_SaveData);
	void LoadFromSaveData(SaveData a_SaveData);
}