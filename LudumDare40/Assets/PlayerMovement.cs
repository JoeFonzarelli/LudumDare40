using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour {

    //run variables
    public float Standardspeed = 10;
    float AirSpeed;
    float translation;
	float direction = 1;

    //jump variables
    public float maxJumpForce = 80;
    float jumpDecayRate;
    float jumpForce;
    bool isGrounded = false, isJumping = false;

    //stats variables
    public int lives = 3;
    public float fat = 0;
    public int score = 0;

	int state = 0, prevState =0;

	public AudioClip[] audio;
	AudioSource sound;
	AudioSource walk;
	// Use this for initialization
	void Start () {
        jumpDecayRate = maxJumpForce / 6.0f;
        jumpForce = maxJumpForce;

        AirSpeed = Standardspeed / 1.5f;
		sound = GetComponent<AudioSource> ();
		walk = transform.GetChild (1).GetComponent<AudioSource> ();
		walk.Play ();
		walk.mute = true;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
        Move();
        Jump();
        Hit();

        GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
		if (fat >= 0 && fat < 25) {
			state = 0;
		}
		if (fat >= 25 && fat < 50) {
			state = 1;
		}
		if (fat >= 50 && fat < 75) {
			state = 2;
		}
		if (fat >= 75 && fat < 100) {
			state = 3;	
		}

		if (prevState != state){
			prevState = state;
			GameObject.Find ("ParticleSystem").transform.GetChild (4).gameObject.SetActive(true);
			GameObject.Find ("ParticleSystem").transform.GetChild (4).gameObject.transform.position = transform.position;
			GameObject.Find ("ParticleSystem").transform.GetChild (4).gameObject.GetComponent<ParticleSystem> ().Play ();
		}
		GetComponent<Animator> ().SetBool ("IsGrounded", isGrounded);

	
	}

    private void Move()
    {
		GameObject.Find ("ParticleSystem").transform.GetChild (5).gameObject.transform.position = GetComponent<SphereCollider> ().center+ transform.position;
        translation = Input.GetAxis("Horizontal");
        translation *= (isGrounded)?Standardspeed:AirSpeed;
        translation *= Time.deltaTime;
		if (translation != 0.0f && isGrounded) {
			walk.mute = false;
			GetComponent<Animator> ().SetBool ("IsMoving", true);

		} else {
			walk.mute = true;
			GameObject.Find ("ParticleSystem").transform.GetChild (5).gameObject.SetActive (true);
			GetComponent<Animator> ().SetBool ("IsMoving", false);
			GameObject.Find ("ParticleSystem").transform.GetChild (5).gameObject.GetComponent<ParticleSystem> ().Play ();
		}

		if(translation != 0) direction = Mathf.Sign(translation);


		transform.Translate(new Vector3(0,0,1) * translation);
		transform.localScale = new Vector3 (1, 1, direction);
		if (direction == -1) {
			GameObject.Find ("ParticleSystem").transform.GetChild (5).gameObject.transform.rotation = Quaternion.Euler(new Vector3(-19.764f, 100.256f, -177.8156f));
		}else GameObject.Find ("ParticleSystem").transform.GetChild (5).gameObject.transform.rotation = Quaternion.Euler(new Vector3(-162.764f, 100.256f, -182.8156f));
    }

    private void Hit()
    {
        if (Input.GetButtonDown("Fire1") && isGrounded)
        {
			GetComponent<Animator> ().SetBool ("IsAttacking", true);
            StartCoroutine(punch());
        }
    }

    IEnumerator punch()
	{ 	
		
        transform.GetChild(0).gameObject.SetActive(true);
		transform.GetChild (0).gameObject.GetComponent<Punch_behavior> ().punch = transform.GetChild (0).gameObject.GetComponent<AudioSource> ();
		transform.GetChild (0).gameObject.GetComponent<Punch_behavior> ().punch.Play ();
        yield return new WaitForSeconds(0.1f);
        transform.GetChild(0).gameObject.SetActive(false);
		GetComponent<Animator> ().SetBool ("IsAttacking", false);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded) { //start
            isJumping = true;

        }
        if (Input.GetButtonUp("Jump")) //reset
        {
            jumpForce = maxJumpForce;
            isJumping = false;

        }

        if (Input.GetButton("Jump") && isJumping) // do
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
            jumpForce -= jumpDecayRate;
            if (jumpForce < 0) jumpForce = 0;
        }


    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Ground")
        {
            isGrounded = true;
			sound.PlayOneShot (audio [1]);
			GameObject.Find ("ParticleSystem").transform.GetChild (0).gameObject.SetActive(true);
			GameObject.Find ("ParticleSystem").transform.GetChild (0).gameObject.transform.position = GetComponent<SphereCollider> ().center+transform.position;
			GameObject.Find ("ParticleSystem").transform.GetChild (0).gameObject.GetComponent<ParticleSystem> ().Play ();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.tag == "Enemy")
        {
			sound.PlayOneShot (audio [2]);
            --lives;
			GameObject.Find ("ParticleSystem").transform.GetChild (2).gameObject.SetActive(true);
			GameObject.Find ("ParticleSystem").transform.GetChild (2).gameObject.transform.position = transform.position;
			GameObject.Find ("ParticleSystem").transform.GetChild (2).gameObject.GetComponent<ParticleSystem> ().Play ();
            Debug.Log(lives);
            if (lives <= 0)
            {
				
				sound.PlayOneShot (audio [3]);
            }
        }
    }

	private void OnTriggerStay(Collider coll){
		if (coll.gameObject.tag == "Ground") {
			isGrounded = true;
		}
	}
}
