using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBar : MonoBehaviour {

	public GameObject door;
	public GameObject camera;
	private Camera cam;
	Vector3 camPosition;
	public GameObject barBackground;
	// Use this for initialization
	void Start () {
		cam = camera.GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		

		camPosition = cam.WorldToScreenPoint(door.transform.position);
		transform.position = camPosition;
		barBackground.transform.position = camPosition;
	}
}
