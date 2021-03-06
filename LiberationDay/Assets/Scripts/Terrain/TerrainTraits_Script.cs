using UnityEngine;
using System.Collections;

public class TerrainTraits_Script : MonoBehaviour {
	// how fast units move through this terrain  (1 by default)
	public float speed = 1.0f;
	// if movement is possible through the terrain (false by default)
	public bool blocks = false;
	// damage bonus for being in this terrain (1 by default)
	public float damageBonus = 1.0f;
	// defensive bonus for being in this terrain (1 by default)
	public float defensiveBonus = 1.0f;

	public int posX;

	public int posY;

	void Start() {
		//Debug.Log (posX + " " + posY);
	}

	void OnMouseDown() {
		if (UnitController.canMove && 
		    UnitController.unit.GetComponent<MegaController>().movementRange.Contains(this.gameObject)) {
			UnitController.move(this.gameObject);
		}
	}
}
