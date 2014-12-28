using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {
	static bool level1 = true;
	public static bool level2 = false;
	static bool level3 = false;
	GameObject flag1;	
	GameObject flag2;
	GameObject flag3;
	// Use this for initialization
	void Start () {
		if(level1) {
			flag1 = Instantiate(Resources.Load("Prefab/flag", typeof(GameObject)), 
			                                   transform.position + new Vector3(0,0,0), transform.rotation) as GameObject;
		}
		if(level2) {
			flag2 = Instantiate(Resources.Load("Prefab/flag", typeof(GameObject)), 
			                    transform.position + new Vector3(-5,0,0), transform.rotation) as GameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
