using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {

	private int lives;
	private int score;
	public GameObject Player;
	private PlayerMovement playerInfo;
	// Use this for initialization
	void Start () {
		playerInfo = Player.GetComponent<PlayerMovement> ();
		lives = playerInfo.lives;
	}

	void Update(){
		switch (lives) {
		case 1:
			GameObject.Find ("life2").SetActive(false);
			GameObject.Find ("life3").SetActive(false);
			break;
		case 2:
			GameObject.Find ("life3").SetActive(false);
			break;
		}
	}
	
	// Update is called once per frame
	void OnGUI () {
		
	}
}
