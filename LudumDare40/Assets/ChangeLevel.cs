using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour {

	public GameObject nextFloor, nextWall;
	private Vector3 nextPos;
	bool changePos = false;
	public GameObject gameover;
	// Use this for initialization
	void Start () {
		nextPos = Camera.main.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameover.activeInHierarchy == true) {
			if (Input.GetButtonDown ("Jump")) {
				SceneManager.LoadScene ("Menu");
			}
		}
		if (changePos && (nextPos - Camera.main.transform.position).magnitude > 0.5f) {
			Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, nextPos, 1 * Time.deltaTime);
		} else {
			nextPos = Camera.main.transform.position;
			changePos = false;


			GameObject.Find ("Main Camera").GetComponent<CameraShake> ().ChangePos ();
		}
	}

	void OnTriggerEnter(Collider col){
		
		if (col.gameObject.tag == "Player") {
			changePos = true;


			nextPos = new Vector3 (nextFloor.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
			GameObject.Find ("DoorBar").transform.position = Camera.main.WorldToScreenPoint (nextWall.transform.position)/2.0f;
			GameObject.Find ("DoorBar").transform.position = new Vector3(GameObject.Find ("DoorBar").transform.position.x,GameObject.Find ("DoorBar").transform.position.y + 50, GameObject.Find ("DoorBar").transform.position.z);
			GameObject.Find ("DoorBarBackground").transform.position = Camera.main.WorldToScreenPoint (nextWall.transform.position)/2.0f;
			GameObject.Find ("DoorBarBackground").transform.position = new Vector3(GameObject.Find ("DoorBarBackground").transform.position.x,GameObject.Find ("DoorBarBackground").transform.position.y + 50, GameObject.Find ("DoorBarBackground").transform.position.z);
		}
	}
}
