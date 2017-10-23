using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {
	
	public AudioClip gameTheme;
	public AudioSource speaker;
	// Use this for initialization
	void Start () {
		speaker = GetComponent<AudioSource> ();
		speaker.clip = gameTheme;
	}

	void OnEnable(){
		GamePlayManagerScript.OnSetGameActiveEvent += PlayThemeMusic;
	}

	void OnDisable(){
		GamePlayManagerScript.OnSetGameActiveEvent -= PlayThemeMusic;
	}
	// Update is called once per frame
	void Update () {
		
		
	}

	void PlayThemeMusic (bool play){
		if (play)
			speaker.Play ();
		else
			speaker.Stop();
		Debug.Log ("Play = " + play);

	}


}
