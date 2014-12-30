using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/*
 * Controller class that contains all the information in the game about the
 * player status. Must be placed in every scene to ensure singleton scene.
 */
public class GameController : MonoBehaviour {

	public static GameController control;

	public static bool stage1;
	public static bool stage2;
	public static bool stage3;
	// Use this for initialization
	void Awake () {
		if(control == null)	{
			DontDestroyOnLoad(gameObject);
			control = this;
		} else if(control != this) {
			Destroy(gameObject);
		}
	}

	public static void save() {
		if(File.Exists(Application.persistentDataPath + "/player.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/player.dat", FileMode.Open);

			PlayerData data = new PlayerData();
			data.stage1 = stage1;
			data.stage2 = stage2;
			data.stage3 = stage3;

			bf.Serialize (file, data);
			file.Close ();
		} else {
			FileStream file = File.Open (Application.persistentDataPath + "/player.dat", FileMode.CreateNew);
			file.Close();
		}
	}

	public static void load() {
		if(File.Exists(Application.persistentDataPath + "/player.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream fs = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(fs);
			fs.Close();

			stage1 = data.stage1;
			stage2 = data.stage2;
			stage3 = data.stage3;
		}
	}
}

[System.Serializable]
class PlayerData
{
	public bool stage1;
	public bool stage2;
	public bool stage3;
}
