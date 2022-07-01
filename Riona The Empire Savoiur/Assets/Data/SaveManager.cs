///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   SaveManager.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/04/07
///   Description       : Saves and Loads game stats in json format
///   Revision History  : 2nd ed. Commented out Debug.Log
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public static class SaveManager
{
	public static string directory = "/SaveData/";
	public static string fileName = "MyData.txt";
	public static bool isLoading = false;
	public static void Save(IEnumerable<ISaveable> a_Saveables)
	{
		SaveData sd = new SaveData();
		foreach (var saveable in a_Saveables)
		{
			saveable.PopulateSaveData(sd);
		}
		string dir = Application.persistentDataPath + directory;
		//Debug.Log(dir);
		if (!Directory.Exists(dir))
		{
			Directory.CreateDirectory(dir);
		}

		string json = JsonUtility.ToJson(sd);
		File.WriteAllText(dir + fileName, json);
	}

	public static void Load(IEnumerable<ISaveable> a_Saveables)
	{
		string fullPath = Application.persistentDataPath + directory + fileName;
		SaveData sd = new SaveData();
		if (File.Exists(fullPath))
		{
			string json = File.ReadAllText(fullPath);
			sd = JsonUtility.FromJson<SaveData>(json);
			foreach (var saveable in a_Saveables)
			{
				saveable.LoadFromSaveData(sd);
			}
		}
		else
		{
			//Debug.Log("Save file doesn't exist");
		}

	}

}
