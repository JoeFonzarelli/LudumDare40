using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {


	private int lives;
	private int score;
	public GameObject Player;

	// Use this for initialization
	void Start () {

	}

	void Update(){

		lives = Player.GetComponent<PlayerMovement>().lives;
		switch (lives) {
		case 1:
			GameObject.Find ("life2").SetActive(false);
			break;
		case 2:
			GameObject.Find ("life3").SetActive(false);
			break;
		default:
			break;
		}

	}
}
