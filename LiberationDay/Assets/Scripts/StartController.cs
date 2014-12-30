using UnityEngine;
using System.Collections;

public class StartController : MonoBehaviour {
	
	public void OnClickPlay() {
		GameController.load ();
		Application.LoadLevel ("LevelSelect");
	}
}
