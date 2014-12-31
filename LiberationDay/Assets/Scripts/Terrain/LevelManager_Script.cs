using UnityEngine;
using System.Collections;
using System.IO;

// Much of this inspiration for this code was taken from 
// http://stackoverflow.com/questions/26147958/creating-a-tiled-map-in-unity-using-a-2d-array-and-a-text-file

// This class is attached to a level manager object that units in the game 
// consult to gather information about legal movement, bonuses, and more.
// Units do this by checking coordinates in the "terrain" array that 
// correspond to actual locations on the map. Each game object in the array
// has a TerrainTraits_Script that hold information about what kind of traits
// are associated with the terrain.

public class LevelManager_Script : MonoBehaviour {
	// rows and columns of map
	public static int rows;
	public static int columns;
	public static int val = 5;

	// this is the array that should be accessed for movement 
	// logic by various units in gameplay to determine legal 
	// movement 
	public static GameObject[,] terrain;
	public static bool[,] units;

	// Use this for initialization
	void Awake () {
		// normally InitializeLevel would be called elsewhere in game.
		// for testing purposes, we're just calling it here.
		// TODO: Move this elsewhere when testing is over.
		InitializeLevel("level0");
	}
	

	// this function takes a text file that represents a map and uses it to 
	// initialize the terrain array and game world
	public void InitializeLevel(string levelName){
		//make string out of map .txt file
		string levelData = File.ReadAllText( Application.dataPath + "/Maps/" + levelName + ".txt");
		string[] levelDataArr = levelData.Split(new string[] {"\n", "\r", "\r\n"}, System.StringSplitOptions.RemoveEmptyEntries);
		rows = int.Parse (levelDataArr[0]);
		columns = int.Parse(levelDataArr[1]);
		terrain = new GameObject[rows, columns];
		units = new bool[rows, columns];
		for(int i = 0; i < rows; i++){
			string[] row =  levelDataArr[i + 2].Split(new string[] {" "}, System.StringSplitOptions.RemoveEmptyEntries);
			for(int j = 0; j < columns; j++){
				// get terrain object that is dependent on number from level .txt file
				GameObject terrainPiece = Instantiate(Resources.Load("Prefab/terrain" + row[j], typeof(GameObject)), 
				                                      transform.position + new Vector3(j,-i,0), transform.rotation) as GameObject;
				// make this parent to prevent tiles from cluttering hierarchy
				terrainPiece.transform.parent = gameObject.transform;
				// update array so it can be accessed by other game objects
				terrain[i,j] = terrainPiece;

			}
		}
	}
}
