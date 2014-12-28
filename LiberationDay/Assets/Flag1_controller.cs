using UnityEngine;
using System.Collections;

public class Flag1_controller : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		LevelSelect.level2 = true;
		Application.LoadLevel ("levelselect");
	}
}
