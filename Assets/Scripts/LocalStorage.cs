using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LocalStorage : MonoBehaviour {

	public int highScore;
	public bool firstRun = true;

	public GameScore gameScore;
	public Menu menu;

	void Start ()
	{
		if (menu == null)
			menu = GetComponent<Menu>();

		GoLoad ();
	}

	public void GoSave () 
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/game.sav");
		SaveData data = new SaveData ();

		// ? Store the designated variables first
		highScore = gameScore.score;

		// ? Then store it to Save data
		data.highScore = highScore;

		bf.Serialize (file, data);
		file.Close ();
	}

	public void FirstRunSave()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/game.sav");
		SaveData data = new SaveData ();

		firstRun = false;
		data.firstRun = firstRun;

		bf.Serialize (file, data);
		file.Close ();
	}

	void GoLoad ()
	{
		if (File.Exists (Application.persistentDataPath + "/game.sav")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/game.sav", FileMode.Open);
			SaveData data = (SaveData) bf.Deserialize (file);
			file.Close ();

			highScore = data.highScore;
			firstRun = data.firstRun;

			if (SceneManager.GetActiveScene().name == "Menu")
				menu.FirstRun();
		}
		else
		{
			highScore = 0;
			firstRun = true;
		}
	}

	public void GoReset ()
	{
		highScore = 0;
		gameScore.score = 0;

		GoSave();
	}
}

[Serializable]
class SaveData
{
	public int highScore;
	public bool firstRun;
}
