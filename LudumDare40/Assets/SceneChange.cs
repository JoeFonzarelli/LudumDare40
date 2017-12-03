using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChange : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		

	public void GoToPlay(){
		Debug.Log ("play");
		SceneManager.LoadScene ("Play", LoadSceneMode.Single);
	}

	public void GoToCredits(){
		Debug.Log ("credits");
		SceneManager.LoadScene ("Credits", LoadSceneMode.Single);
	}

	public void GoToMenu(){
		SceneManager.LoadScene ("Menu", LoadSceneMode.Single);
	}


	public void Exit(){
		#if UNITY_EDITOR

		UnityEditor.EditorApplication.isPlaying = false;

		#else
		Application.Quit ();
		#endif
	}
}
