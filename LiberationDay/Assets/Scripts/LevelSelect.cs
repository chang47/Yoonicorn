using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {
	GameObject flag1;	
	GameObject flag2;
	GameObject flag3;
	// Use this for initialization
	void Start () {
		if(GameController.stage1) {
			flag1 = Instantiate(Resources.Load("Prefab/flag", typeof(GameObject)), 
			                                   transform.position + new Vector3(0,0,0), transform.rotation) as GameObject;
		}
		if(GameController.stage2) {
			flag2 = Instantiate(Resources.Load("Prefab/flag", typeof(GameObject)), 
			                    transform.position + new Vector3(-5,0,0), transform.rotation) as GameObject;
		}
		if (GameController.stage3) {
			flag3 = Instantiate(Resources.Load("Prefab/flag", typeof(GameObject)), 
			                    transform.position + new Vector3(5,0,0), transform.rotation) as GameObject;
		}
	}

	void OnGUI() {
		GUI.color = Color.black;
		GUI.Label(new Rect(10, 100, 100, 30), "flag1 :" +  GameController.stage1);
		GUI.Label(new Rect(10, 120, 100, 30), "flag1 :" +  GameController.stage2);
		GUI.Label(new Rect(10, 140, 100, 30), "flag1 :" +  GameController.stage3);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
