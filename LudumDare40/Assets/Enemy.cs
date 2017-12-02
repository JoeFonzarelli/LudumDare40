using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public GameObject player;
	private Vector3 playerPosition;
	public float speed = 5.0f;
	private float distanceToPlayer;
	public float distanceToAttack;
	private Vector3 direction;

	void calcDirection(){
		direction = Vector3.right * Time.deltaTime * speed * Mathf.Sign(playerPosition.x - transform.position.x);
	}

	// Use this for initialization
	void Start () {
		InvokeRepeating ("calcDirection", 0, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {

        player = GameObject.Find("Player");
		playerPosition = player.transform.position;
		//gameObject.transform.LookAt (player.transform, Vector3.right);
		distanceToPlayer = Vector3.Magnitude (playerPosition - transform.position);
		//flip enemy 
		if (player.transform.position.x > transform.position.x) {
			transform.localScale = new Vector3 (1, 1, 1);
		} else {
			transform.localScale = new Vector3 (-1, 1, 1);
		}


		if (distanceToPlayer < distanceToAttack) {
			Debug.Log ("attacking");
		} else {
			transform.position += direction;
		}



	}
}
