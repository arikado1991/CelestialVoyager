using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {




//singleton instance
	static private SoundManagerScript s_soundManager;
	static public SoundManagerScript GetInstance() {
		return s_soundManager;
	}


	Dictionary <string, AudioClip> musicLibrary;
	Dictionary <string, AudioClip> soundLibrary;

	public AudioSource themeMusicSpeaker;
	ObjectPool speakerPool;

	void OnEnable(){
		GamePlayManagerScript.OnSetGameActiveEvent += LoadThemeMusic ;
		GamePlayManagerScript.OnGameRestartEvent += ResetSpeakers;
		SpeakerScript.OnSoundPlayFinish += DeleteSpeaker;

	

	}

	void OnDisable(){
		//GamePlayManagerScript.OnSetGameActiveEvent -= LoadThemeMusic ;
		//SpeakerScript.OnSoundPlayFinish -= DeleteSpeaker;
	
	}

	// Use this for initialization
	void Awake () {

		if (s_soundManager != null && s_soundManager != this) {
			GameObject.Destroy (this.gameObject);
			return;
		} 
		s_soundManager = this;
	


		speakerPool = new ObjectPool ();
		speakerPool.SetPrefab (Resources.Load ("Prefabs/Effects/Speaker") as GameObject);


		themeMusicSpeaker = transform.Find ("ThemeMusicPlayer").transform.GetComponent<AudioSource> ();
	
		// Load Music and Sound into the libraries

		Object[] tempObjArray;

		musicLibrary = new Dictionary <string, AudioClip> ();

		tempObjArray =  Resources.LoadAll ("Sounds/Music", typeof (AudioClip));
		foreach (var clip in tempObjArray) {
			musicLibrary.Add(clip.name, clip as AudioClip);
//			Debug.Log (clip.name);
		}

		soundLibrary = new Dictionary <string, AudioClip> ();

		tempObjArray = Resources.LoadAll ("Sounds/Noises");
		foreach (var clip in tempObjArray) {
			soundLibrary.Add(clip.name, clip as AudioClip);
			//Debug.Log (clip.name);
		}
	}

	void LoadThemeMusic (bool isGameActive) {


		if (isGameActive) {
			themeMusicSpeaker.clip = musicLibrary["Theme Music"];
			themeMusicSpeaker.volume = 1f;
		} else {
			themeMusicSpeaker.clip = musicLibrary["Theme Music"];
			themeMusicSpeaker.volume = 0.3f;
		}
		if (!themeMusicSpeaker.isPlaying)
			themeMusicSpeaker.Play ();
			
	}
		
	public bool PlaySound (string soundName, Vector3 position, float volume = 1f, bool setNewInstance = true) {

		AudioSource audioSource = null;
		GameObject speaker = null;


		if (soundLibrary.ContainsKey (soundName)) {
			
			if (!setNewInstance){
				
				speaker = speakerPool.Find (soundName + "_speaker");
				if (speaker != null){

					audioSource = speaker.GetComponent<AudioSource> ();
					if (volume == 0) {
						DeleteSpeaker (speaker.name);
					}
				}

			}
			if (volume == 0) {
				return true;
			}
			if (speaker == null) {
				
				speaker = speakerPool.GetAvailableObject (soundName + "_speaker");
				speaker.SetActive (false);
			
				audioSource = speaker.GetComponent <AudioSource> ();
				audioSource.clip = soundLibrary [soundName];
				speaker.SetActive (true);
				audioSource.Play ();


			}

			speaker.transform.position = position;

			if (audioSource != null) {
				
				audioSource.volume = volume;

			}
			else
				Debug.Log ("SoundManager: Somehow the audio source is still null");

			return true;
		}
		return false;
	}

		




	void PlayThemeMusic (bool play){
		if (play)
			themeMusicSpeaker.Play ();
		else
			themeMusicSpeaker.Stop();
	}

	void DeleteSpeaker(string name) {
		
		speakerPool.DestroyObject (name);
	
	}

	void ResetSpeakers (){
		Debug.Log ("Clear all speakers");
		speakerPool.Clear ();
	}
}
