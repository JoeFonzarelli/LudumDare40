using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour {

    public GameObject[] target;
	float step;
	public float threshold = 2;
	public float speed = 2;
    int flipflop = 0;
	// Use this for initialization
	void Start () {
        
        
        
    }
	
	// Update is called once per frame
	void Update () {
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target[flipflop].transform.position, step);

        if (Vector3.Magnitude(transform.position-target[flipflop].transform.position)< threshold)
        {
            flipflop = (flipflop == 0) ? 1 : 0;
        }
		if(gameObject.tag == "Enemy")
        {
			gameObject.GetComponent<Animator> ().SetBool ("IsMoving", true);
        }
		
	}

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
