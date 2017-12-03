using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_script : MonoBehaviour {

    public float life;
    public float FatNeeded;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("increaseLife", 0, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void increaseLife(){
		if (life < 2) {
			life += 0.2f;
		}
	}
}
