using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Punch_behavior : MonoBehaviour
{

    public AudioSource punch;
    public AudioClip[] clips;
    // Use this for initialization
    void Start()
    {
        punch = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        punch = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            punch.PlayOneShot(clips[3]);
            --col.gameObject.GetComponent<Enemy>().life;
            GameObject.Find("ParticleSystem").transform.GetChild(4).gameObject.SetActive(true);

            Vector3 pos;

            pos = col.gameObject.transform.position;
            pos.y++;

            GameObject.Find("ParticleSystem").transform.GetChild(4).gameObject.transform.position = pos;
            GameObject.Find("ParticleSystem").transform.GetChild(4).gameObject.GetComponent<ParticleSystem>().Play();

            GameObject.Find("Main Camera").GetComponent<CameraShake>().isShaking = true;
            if (col.gameObject.GetComponent<Enemy>().life <= 0)
            {
				col.gameObject.GetComponent<Animator> ().SetBool ("IsAttacking", false);
				col.gameObject.GetComponent<Animator> ().SetBool ("IsMoving", false);
				col.gameObject.GetComponent<Animator> ().SetBool ("IsDead", true);
                GameObject.Find("ParticleSystem").transform.GetChild(2).gameObject.SetActive(true);
                GameObject.Find("ParticleSystem").transform.GetChild(2).gameObject.transform.position = pos;
                GameObject.Find("ParticleSystem").transform.GetChild(2).gameObject.GetComponent<ParticleSystem>().Play();
                punch.PlayOneShot(clips[2]);
                transform.parent.gameObject.GetComponent<PlayerMovement>().fat += col.gameObject.GetComponent<Enemy>().fatGiven;
                Destroy(col.gameObject);
            }
        }

        if (col.gameObject.tag == "wall")
        {

            GameObject.Find("ParticleSystem").transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("ParticleSystem").transform.GetChild(1).gameObject.transform.position = GetComponent<BoxCollider>().center + transform.position;
            GameObject.Find("ParticleSystem").transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
            punch.PlayOneShot(clips[0]);
            ;
            if (col.gameObject.GetComponent<wall_script>().FatNeeded <= GetComponentInParent<PlayerMovement>().fat) {

                --col.gameObject.GetComponent<wall_script>().life;
            }
            if (col.gameObject.GetComponent<wall_script>().life <= 0)
            {
                GetComponentInParent<PlayerMovement>().fat -= col.gameObject.GetComponent<wall_script>().FatNeeded;
                Debug.Log(col.transform.childCount);
                for (int i = 3; i >= 0; i--)
                {
                    col.gameObject.GetComponent<Transform>().GetChild(i).GetComponent<Rigidbody>().AddForce(Vector3.right * Random.Range(-3, 3) + Vector3.up * Random.Range(0, 4));
                    col.gameObject.GetComponent<Transform>().GetChild(i).GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), Random.Range(-3, 3));
                    col.gameObject.GetComponent<Transform>().GetChild(i).GetComponent<BoxCollider>().enabled = false;
                    col.gameObject.GetComponent<Transform>().GetChild(i).parent = null;
                    punch.PlayOneShot(clips[6]);

                    //Destroy (GameObject.Find ("DoorBar"));
                    //Destroy (GameObject.Find ("DoorBarBackground"));


                }
                Destroy(col.gameObject);
            }
        }
    }
}