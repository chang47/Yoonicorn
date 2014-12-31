using UnityEngine;
using System.Collections;
/*
 * Battle system controller. Takes control of fighting and storing
 * selected unit to allow battle and movement.
 */
public class UnitController : MonoBehaviour {

	public static GameObject unit;
	public static bool unitMove;
	public static bool unitAttack;
	public static bool inUse;

	public static void battle(GameObject enemy) {
		//receives enemy game object and fights
		Debug.Log ();
		Debug.Log ();
	}

	public static void move(GameObject terrain) {
		//movement method
	}

}
