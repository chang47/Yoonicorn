using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MegaController : MonoBehaviour {
	private bool showMenu = false;
	private int hp = 10;
	private int attack = 5;
	private int move = 3;
	private HashSet<GameObject> movementRange;

	// Use this for initialization
	void Start () {
		//LevelManager_Script.val = 123213;
		//Debug.Log (LevelManager_Script.val);
		LevelManager_Script.units [(int)transform.position.x, (int)-transform.position.y] = true;
		movementRange = new HashSet<GameObject>();
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
			//transform.position = new Vector2(transform.position.x + 1, transform.position.y + 1);

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

	void drawMovementRange() {
				// TODO: figure out NOWNOWNOW.
	}

	void calculateMovementRange() {
		Stack<GameObject> s = new Stack<GameObject> ();
		int unitX = (int)transform.position.x;
		int unitY = (int)transform.position.y;
		s.Push (LevelManager_Script.terrain [unitX, unitY]);
		while (s.Count > 0) {
			GameObject current = s.Pop();
			int currentX = (int) current.transform.position.x;
			int currentY = (int) -current.transform.position.y;
			int dx = Math.Abs(currentX - unitX);
			int dy = Math.Abs(currentY - unitY);

			if (!movementRange.Contains(current) && dx + dy <= move && 
			    (current == LevelManager_Script.terrain[unitX, unitY] || validTile(current))) {
				movementRange.Add (current);
				if (currentY > 0) {
					s.Push(LevelManager_Script.terrain[currentX, currentY - 1]);
				} else if (currentY < LevelManager_Script.rows - 1) {
					s.Push(LevelManager_Script.terrain[currentX, currentY + 1]);
				} else if (currentX > 0) {
					s.Push (LevelManager_Script.terrain[currentX - 1, currentY]);
				} else if (currentX < LevelManager_Script.columns - 1) {
					s.Push(LevelManager_Script.terrain[currentX + 1, currentY]);
				}
			}
		}
	}

	bool validTile(GameObject tile) {
		return !tile.GetComponent<TerrainTraits_Script>().blocks && 
			!LevelManager_Script.units[(int)tile.transform.position.x, (int)-tile.transform.position.y];  
	}
}
