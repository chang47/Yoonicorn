﻿using UnityEngine;
using System.Collections;
/*
 * Battle system controller. Takes control of fighting and storing
 * selected unit to allow battle and movement.
 */
public class UnitController : MonoBehaviour {

	// The unit being controlled
	public static MegaController unit;

	// flag for if move state is entered.
	public static bool canMove;

	// flag for if attack state is entered
	public static bool canAttack;

	// flag to see if any units have been selected for control.
	public static bool inUse;

	// The enemy unit being attacked.
	public static MegaController enemyUnit;

	public static bool attackMenu = false;
	/*
	void OnGUI() {
		if(attackMenu) {
			Rect window = GUI.Window(0, new Rect(Screen.width - 320, Screen.height - 300, 300, 300), attackInfo, "Battle stats");
		}
	}

	void attackInfo(int aID) {
		if(GUI.Button(new Rect(0, 20, 300, 150), "Yes")) {
			enemyUnit.remove();
			enemyUnit = null;
			attackMenu = false;
		}if(GUI.Button(new Rect(0, 170, 300, 150), "No")) {
			attackMenu = false;
		}
	}

*/	public static void battle(MegaController enemy) {
		enemyUnit = enemy;
		attackMenu = true;
	}

	public static void move(GameObject terrain) {
		TerrainTraits_Script tScript = terrain.GetComponent<TerrainTraits_Script> ();

		LevelManager_Script.units [(int)-unit.transform.position.y, (int)unit.transform.position.x] = false;
		unit.transform.position = new Vector2 (tScript.posX, -tScript.posY);
		LevelManager_Script.units [(int)-unit.transform.position.y, (int)unit.transform.position.x] = true;

		foreach (GameObject tile in unit.GetComponent<MegaController>().movementRange) {
			tile.transform.GetChild (0).renderer.material.color = Color.white;
		}

		unit.GetComponent<MegaController> ().movementRange.Clear ();
		canMove = false;
	}

}
