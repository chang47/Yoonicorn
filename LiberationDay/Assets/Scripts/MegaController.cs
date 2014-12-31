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
	private bool foundEnemy;

	// Use this for initialization
	void Start () {
		//LevelManager_Script.val = 123213;
		//Debug.Log (LevelManager_Script.val);
		LevelManager_Script.units [(int)-transform.position.y, (int)transform.position.x] = true;
		/*for (int i = 0; i < LevelManager_Script.rows; i++) {
			for (int j = 0; j < LevelManager_Script.columns; j++) {
				if (LevelManager_Script.units [i, j]) {
					Debug.Log ("i: " + i + "\nj: " + j);
				}
			}
		}*/
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
		if(foundEnemy) {

			bool[,] units = LevelManager_Script.units;
			int posX = (int)transform.position.x;
			int posY = (int)-transform.position.y;

			if(LevelManager_Script.units[posX - 1, posY]) {
				GUI.Label(new Rect(posX - 1, posY, 20, 20), "location :" + units[posX - 1, posY]);
				if(GUI.Button(new Rect(posX - 1 * 32, posY, 100, 200), "left")) {
					foundEnemy = false;
				}
				//left
			} else if(LevelManager_Script.units[posX + 1, posY]) {
				//right
				GUI.Label(new Rect(posX + 1, posY, 20, 20), "location :" + units[posX + 1, posY]);
				if(GUI.Button(new Rect(posX + 1, posY, 100, 200), "right")) {
					foundEnemy = false;
				}
			} else if(LevelManager_Script.units[posX, posY - 1]) {
				//above
				GUI.Label(new Rect(posX, posY - 1, 20, 20), "location :" + units[posX, posY - 1]);
				if(GUI.Button(new Rect(posX, posY - 1, 100, 200), "above")) {
					foundEnemy = false;
				}
			} else if(LevelManager_Script.units[posX, posY + 1]) {
				//behind
				GUI.Label(new Rect(posX, posY + 1, 20, 20), "location :" + units[posX, posY + 1]);
				if(GUI.Button(new Rect(posX, posY + 1, 100, 200), "below")) {
					foundEnemy = false;
				}
			}
		}
	}

	void draw(int aID) {
		if(GUI.Button (new Rect (0,20,100,50), "Move")){
			//transform.position = new Vector2(transform.position.x + 1, transform.position.y + 1);
			drawMovementRange();
			//Debug.Log("movementRange: " + movementRange.Count);

			UnitController.unit = this.gameObject;
			UnitController.unitMove = true;
		}else if(GUI.Button (new Rect (0,70,100,50), "Attack")){
			//attack();
			bool[,] units = LevelManager_Script.units;
			int posX = (int)transform.position.x;
			int posY = (int)-transform.position.y;
			foundEnemy = true;

		}else if(GUI.Button (new Rect (0,120,100,50), "Cancel")){
			showMenu = false;
		}
	}

	void OnMouseDown() {
		Debug.Log(LevelManager_Script.units[(int)-transform.position.y, (int)transform.position.x]);
		showMenu = !showMenu;
		if(showMenu) {
			//Debug.Log("true");
		} else {
			//Debug.Log("false");
		}
	}

	void drawMovementRange() {
		calculateMovementRange ();
		foreach (GameObject tile in movementRange) {
			tile.transform.GetChild(0).renderer.material.color = Color.green;
		}
	}

	void calculateMovementRange() {
		Stack<GameObject> s = new Stack<GameObject> ();
		int unitX = (int)transform.position.x;
		int unitY = (int)-transform.position.y;
		s.Push (LevelManager_Script.terrain [unitY, unitX]);
		while (s.Count > 0) {
			GameObject current = s.Pop();
			int currentX = (int) current.transform.position.x;
			int currentY = (int) -current.transform.position.y;
			int dx = Math.Abs(currentX - unitX);
			int dy = Math.Abs(currentY - unitY);
			int distance = dx + dy;

			//Debug.Log("unitX: " + unitX + "\nunitY: " + unitY + "\ncurrentX: " + currentX + "\ncurrentY: " + currentY);
			//Debug.Log("distance: " + distance + "\nalready contained: " + movementRange.Contains(current) + "\nvalidTile: " + validTile(current));
			if (!movementRange.Contains(current) && distance <= move && 
			    (current == LevelManager_Script.terrain[unitY, unitX] || validTile(current))) {
				movementRange.Add (current);
				if (currentY > 0) {
					s.Push(LevelManager_Script.terrain[currentY - 1, currentX]);
				}

				if (currentY < LevelManager_Script.rows - 1) {
					s.Push(LevelManager_Script.terrain[currentY + 1, currentX]);
				}

				if (currentX > 0) {
					s.Push (LevelManager_Script.terrain[currentY, currentX - 1]);
				}

				if (currentX < LevelManager_Script.columns - 1) {
					s.Push(LevelManager_Script.terrain[currentY, currentX + 1]);
				}
			}
		}
	}

	bool validTile(GameObject tile) {
		bool res = !tile.GetComponent<TerrainTraits_Script> ().blocks && 
			!LevelManager_Script.units [(int)-tile.transform.position.y, (int)tile.transform.position.x];
		//Debug.Log("validTile returned: " + res);
		return res;  
	}
}
