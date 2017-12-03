using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class FatBat : MonoBehaviour {

	public float maxFat;
	public float fat = 0.0f;
	public GameObject Player;
	private PlayerMovement playerInfo;
	private float scale;

	// Use this for initialization
	void Start () {
		playerInfo = Player.GetComponent<PlayerMovement> ();
		fat = playerInfo.fat;


	}
	
	// Update is called once per frame
	void Update () {
		fat = playerInfo.fat;
	}

	void OnGUI(){
		//GetComponent<Image> ().sprite.bounds.size.Set(fat / maxFat,1,1) ;
		scale = fat / maxFat;
		if( scale > 1.0f ) scale = 1.0f;
		transform.localScale = new Vector3(scale,1,1);
	}
}
