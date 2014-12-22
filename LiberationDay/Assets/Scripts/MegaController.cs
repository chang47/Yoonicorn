using UnityEngine;
using System.Collections;

public class MegaController : MonoBehaviour {
	private bool showMenu = false;
	private int hp = 10;
	private int attack = 5;
	private int move = 3;
	// Use this for initialization
	void Start () {
		//LevelManager_Script.val = 123213;
		//Debug.Log (LevelManager_Script.val);
		LevelManager_Script.units [(int)transform.position.x, (int)-transform.position.y] = (int)transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {
		if(showMenu) {
			Event e = Event.current;
			Rect windowPos = GUI.Window(0, new Rect(0, 0, 100, 170), draw, "Options:");

			if(e.type == EventType.MouseDown && !windowPos.Contains(e.mousePosition)) {
				showMenu = false;
			}
		}
	}

	void draw(int aID) {
		if(GUI.Button (new Rect (0,20,100,50), "Move")){
			transform.position = new Vector2(transform.position.x + 1, transform.position.y + 1);
			showMenu = false;
		}else if(GUI.Button (new Rect (0,70,100,50), "Attack")){
			//Options code goes here
		}else if(GUI.Button (new Rect (0,120,100,50), "Cancel")){
			showMenu = false;
		}
	}

	void OnMouseDown() {
		Debug.Log(LevelManager_Script.units[(int)transform.position.x, (int)-transform.position.y]);
		showMenu = !showMenu;
		if(showMenu) {
			Debug.Log("true");
		} else {
			Debug.Log("false");
		}
	}
}
