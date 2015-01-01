using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
/**
 * Flow order:
 * 1. User clicks on unit, the inUse flag is unitMove flag is turned on
 * 2. Two options
 * 	a. User clicks to move unit location
 * 	b. User clicks on cancel button to unselect
 * 3. After moving, check to see if there are units that can be attacked around and
 *    turn on showMenu flag and the unitAttack flag if nearby enemies. Maybe cache the enemies
 *    to save re-checking enemy location
 * 4. Three options:
 * 	a. Wait - set endTurn flag to true
 * 	b. Attack - attacks ones of the surrounding enemies and set the endTurn, canAttack flag to true
 *  c. Cancel - moves unit back to starting position and allows moving again
 * 
 */
public class MegaController : MonoBehaviour {
	private bool showMenu = false;
	private int hp = 10;
	private int attack = 5;
	private int move = 3;
	public HashSet<GameObject> movementRange;
	private bool foundEnemy;
	private bool turnEnd;
	private int posX;
	private int	posY;
	private bool attackMenu = false;

	// Use this for initialization
	void Start () {
		//LevelManager_Script.val = 123213;
		//Debug.Log (LevelManager_Script.val);
		LevelManager_Script.units [(int)-transform.position.y, (int)transform.position.x] = true;
		posX = (int)-transform.position.x;
		posY = (int)transform.position.y;
		/*for (int i = 0; i < LevelManager_Script.rows; i++) {
			for (int j = 0; j < LevelManager_Script.columns; j++) {
				if (LevelManager_Script.units [i, j]) {
					Debug.Log ("i: " + i + "\nj: " + j);
				}
			}
		}*/
		movementRange = new HashSet<GameObject>();
	}

	void OnGUI () {
		//unit finished move

		if(showMenu) { //display after finsih moving unit
			Rect unitOptions = GUI.Window(0, new Rect(0, 0, 100, 170), option, "Command");
			//Event e = Event.current;
			//Rect windowPos = GUI.Window(0, new Rect(0, 0, 100, 170), draw, "Options:");

			//if(e.type == EventType.MouseDown && !windowPos.Contains(e.mousePosition)) {
			//	showMenu = false;
			//}
		}
	}

	//@TODO - figure out how to add new labels
	void option(int aID) {
		if(GUI.Button(new Rect(0, 20, 100, 50), "wait")) {
			UnitController.unit.turnEnd = true;
			showMenu = false;
		}
		if(false){ // inRange() there are nearby enemies
			if(GUI.Button(new Rect(0, 70, 100, 50), "attack")) {
				UnitController.unitAttack = true; //allows attacking and probably highlighting unit that can attack
				labels();
			}
		}
	}

	void labels() {
		GUI.Label (new Rect (0, 200, 100, 30), "attacking now");
	}

	void draw(int aID) {
		if(GUI.Button (new Rect (0,20,100,50), "Move")){
			//transform.position = new Vector2(transform.position.x + 1, transform.position.y + 1);
			drawMovementRange();
			//Debug.Log("movementRange: " + movementRange.Count);

			UnitController.unit = this;
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

	//@TODO - 
	void OnMouseDown() {
		Debug.Log(LevelManager_Script.units[(int)-transform.position.y, (int)transform.position.x]);
		showMenu = !showMenu;

		//Behavior of units.
		if(UnitController.inUse) {
			//Attacks the unit within range
			if(inRange(UnitController.unit) && UnitController.unitAttack) { //&& UnitController.unitAttack //after move, atk phase
				Destroy(this.gameObject);

				//De-selects the unit and 
				UnitController.unitAttack = false;
				UnitController.unitMove = false;
				UnitController.unit.turnEnd = true;
			}
			UnitController.inUse = false;
		} else {
			//sets the selected unit to be the one to be controlled
			UnitController.unit = this;
			UnitController.inUse = true;
			UnitController.unitMove = true;
		}
	}

	//Calculates if the enemy unit being clicked is next to the user unit.
	bool inRange(MegaController unit) {
		if((this.posX - 1 == unit.posX && this.posY == unit.posY) ||
		   (this.posX + 1 == unit.posX && this.posY == unit.posY) ||
		   (this.posX == unit.posX && this.posY - 1 == unit.posY) ||
		   (this.posX == unit.posX && this.posY + 1 == unit.posY)) {
			return true;
		} else {
			return false;
		}
	}

	void attackEnemy() {
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

	void drawMovementRange() {
		int unitX = (int)transform.position.x;
		int unitY = (int)-transform.position.y;
		calculateMovementRange (LevelManager_Script.terrain[unitY, unitX], 0, unitX, unitY);
		foreach (GameObject tile in movementRange) {
			tile.transform.GetChild(0).renderer.material.color = Color.green;
		}
	}

	void calculateMovementRange(GameObject tile, int distance, int unitX, int unitY) {
		if (!movementRange.Contains(tile) && distance <= move && 
		    (tile == LevelManager_Script.terrain[unitY, unitX] || validTile(tile))) {
			movementRange.Add (tile);
			int currentX = (int) tile.transform.position.x;
			int currentY = (int) -tile.transform.position.y;

			if (currentY > 0) {
				calculateMovementRange(LevelManager_Script.terrain[currentY - 1, currentX], distance + 1, unitX, unitY);
			}
			if (currentY < LevelManager_Script.rows - 1) {
				calculateMovementRange(LevelManager_Script.terrain[currentY + 1, currentX], distance + 1, unitX, unitY);
			}

			if (currentX > 0) {
				calculateMovementRange(LevelManager_Script.terrain[currentY, currentX - 1], distance + 1, unitX, unitY);
			}

			if (currentX < LevelManager_Script.columns - 1) {
				calculateMovementRange(LevelManager_Script.terrain[currentY, currentX + 1], distance + 1, unitX, unitY);
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
