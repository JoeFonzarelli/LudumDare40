using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Punch_behavior : MonoBehaviour {

	public AudioSource punch;
	public AudioClip[] clips; 
	// Use this for initialization
	void Start () {
		punch = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake(){
		punch = GetComponent<AudioSource> ();
	}

    void OnTriggerEnter(Collider col)
    {
		if (col.gameObject.tag == "Enemy") {
			punch.PlayOneShot (clips [3]);
            --col.gameObject.GetComponent<Enemy>().life;
			Debug.Log ("Hola");
			GameObject.Find ("Main Camera").GetComponent<CameraShake> ().isShaking = true;
            if (col.gameObject.GetComponent<Enemy>().life <= 0)
            {
				punch.PlayOneShot (clips [2]);
                transform.parent.gameObject.GetComponent<PlayerMovement>().fat += col.gameObject.GetComponent<Enemy>().fatGiven;
                //if (transform.parent.gameObject.GetComponent<PlayerMovement>().fat > 1) transform.parent.gameObject.GetComponent<PlayerMovement>().fat = 1;
                Destroy(col.gameObject);
            }
		}

        if (col.gameObject.tag == "wall")
        {
			punch.PlayOneShot (clips [0]);
            --col.gameObject.GetComponent<wall_script>().life;
            if (col.gameObject.GetComponent<wall_script>().life <= 0)
            {
                Debug.Log(col.transform.childCount);
                for (int i=3; i>=0; i--)
                {
                    col.gameObject.GetComponent<Transform>().GetChild(i).GetComponent<Rigidbody>().AddForce(Vector3.right * Random.Range(-3, 3) + Vector3.up * Random.Range(0, 4));
                    col.gameObject.GetComponent<Transform>().GetChild(i).GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), Random.Range(-3, 3));
                    col.gameObject.GetComponent<Transform>().GetChild(i).GetComponent<BoxCollider>().enabled = false;
                    col.gameObject.GetComponent<Transform>().GetChild(i).parent = null;
					punch.PlayOneShot (clips [6]);

					//Destroy (GameObject.Find ("DoorBar"));
					//Destroy (GameObject.Find ("DoorBarBackground"));


                }
                Destroy(col.gameObject);
            }
        }
    }
}
