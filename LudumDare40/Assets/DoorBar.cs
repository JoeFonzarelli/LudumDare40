using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBar : MonoBehaviour {

	public GameObject door;
	public GameObject camera;
	private Camera cam;
	Vector3 camPosition;
	public GameObject barBackground;
	private float scale;

	private float maxLive;
	// Use this for initialization
	void Start () {
		cam = camera.GetComponent<Camera> ();
		maxLive = door.GetComponent<wall_script> ().life;
	}
	
	// Update is called once per frame
	void Update () {
		

		camPosition = cam.WorldToScreenPoint(door.transform.position);
		transform.position = camPosition;
		barBackground.transform.position = camPosition;

		scale =  door.GetComponent<wall_script> ().life/ maxLive;
		transform.localScale = new Vector3 (1, scale, 1);
		Debug.Log (scale);

		if (scale <= 0) {
			Destroy (gameObject);
			Destroy (barBackground);
		}

	}
}
