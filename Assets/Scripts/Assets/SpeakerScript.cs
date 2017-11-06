using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerScript : MonoBehaviour {
	AudioSource audioSource;
	float playDuration;
	public static event EventManagerScript.GetValueDelegate<string> OnSoundPlayFinish;
	// Use this for initialization
	void Awake () {
		playDuration = -1f;
	}

	void OnEnable() {
		audioSource = GetComponent <AudioSource> ();
		if (audioSource == null) {
			Debug.Log ("SpeakerScript: No audioSource component found!");
			return;
		}
		if (audioSource.clip == null) {
			gameObject.SetActive (false);
			return;
		}

		playDuration = audioSource.clip.length + 1f;
	}

	void OnDisable(){
		
	}
	// Update is called once per frame
	void Update () {
		playDuration -= Time.deltaTime;
//		Debug.Log (playDuration);
		if (playDuration != -1 && playDuration <= 0) {
			OnSoundPlayFinish (this.gameObject.name);
			gameObject.SetActive (false);
		}
	}


}
