using UnityEngine;
using System.Collections;

/*
 * Debugger used to test level unlock features
 */
public class Adjuster : MonoBehaviour {
	
	void OnGUI() {
		if(GUI.Button(new Rect(10, 180, 100, 30), "Flag 1")) {
			GameController.stage1 = !GameController.stage1;
		}
		if(GUI.Button(new Rect(10, 210, 100, 30), "Flag 2")) {
			GameController.stage2 = !GameController.stage2;
		}
		if(GUI.Button(new Rect(10, 240, 100, 30), "Flag 3")) {
			GameController.stage3 = !GameController.stage3;
		}
		if(GUI.Button(new Rect(10, 270, 100, 30), "load stage")) {
			Application.LoadLevel("LevelSelect");
		}
		if(GUI.Button(new Rect(10, 300, 100, 30), "save")) {
			GameController.save();
		}
		if(GUI.Button(new Rect(10, 330, 100, 30), "load")) {
			GameController.load();
		}
	}
}
