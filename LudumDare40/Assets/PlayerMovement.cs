using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour {

    //run variables
    public float Standardspeed = 10;
    float AirSpeed;
    float translation;

    //jump variables
    public float maxJumpForce = 80;
    float jumpDecayRate;
    float jumpForce;
    bool isGrounded = false, isJumping = false;

    //stats variables
    public int lives = 3;
    public float fat = 0;
    public int score = 0;

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
        Move();
        Jump();
        Hit();

        GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);

	}

    private void Move()
    {
		
        translation = Input.GetAxis("Horizontal");
        translation *= (isGrounded)?Standardspeed:AirSpeed;
        translation *= Time.deltaTime;
		if (translation != 0.0f && isGrounded) {
			walk.mute = false;
		}
		else walk.mute = true;
		float direction = Mathf.Sign(translation);


		transform.Translate(Vector3.right * translation);
		transform.localScale = new Vector3 (direction, 1, 1);
    }

    private void Hit()
    {
        if (Input.GetButtonDown("Fire1") && isGrounded)
        {
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
			sound.PlayOneShot (audio [4]);
            --lives;
            Debug.Log(lives);
            if (lives <= 0)
            {
				sound.PlayOneShot (audio [5]);
            }
        }
    }

	private void OnTriggerStay(Collider coll){
		if (coll.gameObject.tag == "Ground") {
			isGrounded = true;
		}
	}
}
