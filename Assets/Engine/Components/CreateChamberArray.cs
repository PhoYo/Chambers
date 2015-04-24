using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

[Serializable]
public class CreateChamberArray : MonoBehaviour {

	public int ChamberSize;
	public static int arraySize;
	public Sprite[] ChamberSprite;
	public string LevelName;

	public static Chamber[,] ChamberArray = new Chamber[8,8];
	private string LevelData;

	private StreamWriter sw;
	private StreamReader sr;

	//create player Object
	public Player player1 = new Player ();

	// static level object
	public static Level level = new Level ();
	

	/// <summary>
	/// create players method
	/// </summary>
	/// <param name= number of Players</param>
	private void CreatePlayer (int numberOfPlayers)
	{

		for (int i = 0; i < numberOfPlayers; i++) 
		{
			GameObject oplayer = (GameObject)Instantiate (Resources.Load ("Prefab/player"), new Vector2 (1, 1), Quaternion.identity);

			player1.Name = "Player 1";
			player1.Health = 100;


			SpriteRenderer spriteRenderer = oplayer.GetComponent<SpriteRenderer>();
			spriteRenderer.sortingLayerName = "Player";
				
		}


	}


	void Start ()
	{
		// create the player 
		CreatePlayer (1);

	}

	void Update ()
	{
		//clear the array and make the level
		if (Input.GetKey ("l")) 
		{
			ClearArray();
			CreateLevel();
		}
	}

	/// <summary>
	/// Clears the array.
	/// </summary>
	void ClearArray ()
	{

		Array.Clear (ChamberArray, 0, ChamberArray.Length);
	
	}

	/// <summary>
	/// Creates the level data.
	/// </summary>
	void CreateLevelData ()
	{

		LevelData = null;

		//Create level data
		foreach (Chamber cham in ChamberArray) {
			LevelData += cham.Type;
		}


		//Debug
		Debug.Log(LevelData);
	}
	
	/// <summary>
	/// Creates the level.
	/// </summary>
	public void CreateLevel ()
	{
		// if the level aleady exists
		if (GameObject.Find (LevelName) != null) 
		{
			Debug.Log("gameobject exists");
			Destroy(GameObject.Find (LevelName));
		}

		// name the level
		level.Name = LevelName;

		// new gameObject for the level content 
		GameObject GO_level = new GameObject();
		GO_level.name = level.Name;

		// 2D array for the chamber location
		for (int i=0; i<ChamberSize; i++) {
			for (int j=0; j<ChamberSize; j++) {


				//Create chamber
				Chamber chamber = new Chamber();
				chamber.Name = "chamber" + "_" + i + "_" + j;
				chamber.Type = UnityEngine.Random.Range(0,5);

				//Create chamber array
				ChamberArray [i, j] = chamber;

				//instantite gameobject loaded from the resources folder, set the sprite and the name
				GameObject GOBrick = (GameObject)Instantiate(Resources.Load("Prefab/brick"), new Vector2(i,j),Quaternion.identity);
				GOBrick.GetComponent<SpriteRenderer>().sprite = ChamberSprite[chamber.Type];
				GOBrick.name = "chamber" + "_" + i + "_" + j;

				//set the transform of the chamber class
				chamber.Object = GOBrick.transform;

				//set the parent of chamber to the level gameobject
				GOBrick.transform.parent = GO_level.transform;


				SpriteRenderer spriteRenderer = GOBrick.GetComponent<SpriteRenderer>();
				spriteRenderer.sortingLayerName = "Chambers";
			}
		}
			

		// add chambers to level array
		level.LevelChambers = ChamberArray;

		//Debug.Log (level.LevelChambers);

		// add chamber types to the level data string
		CreateLevelData ();

	}


	////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////// SAVE AND LOAD METHODS ///////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////////

	/// <summary>
	/// Saves the data to file.
	/// </summary>
	/// <param name="fileName">File name.</param>
	public void SaveDataToFile (string fileName)
	{
		sw = new StreamWriter(fileName + ".txt");   //The file is created or Overwritten outside the Assests Folder.
		sw.WriteLine(LevelData);
		sw.Flush();
		Debug.Log ("Level Saved");
	}
	
	/// <summary>
	/// Loads the level data.
	/// </summary>
	/// <param name="fileName">File name.</param>
	void LoadLevelData(string fileName)
	{
		//sr = new StreamWriter(fileName + ".txt");
	}

}




