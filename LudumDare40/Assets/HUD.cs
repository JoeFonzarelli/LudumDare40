using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {


	private int lives;
	private int score;
	public GameObject Player, life3, life2;

	// Use this for initialization
	void Start () {
		life2 = GameObject.Find ("life2");
		life3 = GameObject.Find ("life3");
	}

	void Update(){

		lives = Player.GetComponent<PlayerMovement>().lives;
		switch (lives) {
		case 1:
			life2.SetActive(false);
			break;
		case 2:
			/*Debug.Log (GameObject.Find ("life3") == null);
			foreach(GameObject gb in GameObject.FindGameObjectsWithTag("GameController")){
				Debug.Log("____ "+ gb.name);
			}*/
			life3.SetActive(false);
			break;
		default:
			break;
		}

	}
}
