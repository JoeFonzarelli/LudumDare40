﻿using System.Collections;
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
        Debug.Log(col.gameObject.name);
    }
}
