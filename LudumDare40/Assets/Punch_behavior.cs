using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch_behavior : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
		if (col.gameObject.tag == "Enemy") {
            --col.gameObject.GetComponent<Enemy>().life;
            if (col.gameObject.GetComponent<Enemy>().life <= 0)
            {
                transform.parent.gameObject.GetComponent<PlayerMovement>().fat += col.gameObject.GetComponent<Enemy>().fatGiven;
                Destroy(col.gameObject);
            }
		}
    }
}
