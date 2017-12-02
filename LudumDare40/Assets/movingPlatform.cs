using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour {

    public GameObject[] target;
    float speed, step, threshold;

    int flipflop = 0;
	// Use this for initialization
	void Start () {
        speed = 2;
        threshold = 2;
        
    }
	
	// Update is called once per frame
	void Update () {
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target[flipflop].transform.position, step);

        if (Vector3.Magnitude(transform.position-target[flipflop].transform.position)< threshold)
        {
            flipflop = (flipflop == 0) ? 1 : 0;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy") other.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent == transform) other.transform.parent = null;
    }
}
