using UnityEngine;
using System.Collections;
/*
 * Battle system controller. Takes control of fighting and storing
 * selected unit to allow battle and movement.
 */
public class UnitController : MonoBehaviour {

	// The unit being controlled
	public static GameObject unit;

	// flag for if move state is entered.
	public static bool unitMove;

	// flag for if attack state is entered
	public static bool unitAttack;

	// flag to see if any units have been selected for control.
	public static bool inUse;

	public static void battle(GameObject enemy) {
		//receives enemy game object and fights
	}

	public static void move(GameObject terrain) {
		//movement method
	}

}
