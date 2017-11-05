using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

	class SpeakerPool {
		public Dictionary <string, GameObject> deployedSpeakers;
		public Queue inactiveSpeakers;
	}

	SpeakerPool speakerPool;

//singleton instance
	static private SoundManagerScript s_soundManager;
	static public SoundManagerScript GetInstance() {
		return s_soundManager;
	}


	Dictionary <string, AudioClip> musicLibrary;
	Dictionary <string, AudioClip> soundLibrary;




	public AudioSource themeMusicSpeaker;

	// Use this for initialization
	void Awake () {

		if (s_soundManager != null && s_soundManager != this) {
			GameObject.Destroy (this.gameObject);
			return;
		} 
		s_soundManager = this;


		speakerPool = new SpeakerPool ();
		speakerPool.deployedSpeakers = new Dictionary<string, GameObject> ();
		speakerPool.inactiveSpeakers = new Queue ();


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

		AudioSource audioSource;
		GameObject speaker;


		if (soundLibrary.ContainsKey (soundName)) {
			
			if (!setNewInstance && speakerPool.deployedSpeakers.ContainsKey (soundName + "_speaker")) {

				speaker = speakerPool.deployedSpeakers [soundName + "_speaker"];
				audioSource = speaker.GetComponent<AudioSource> ();

			} else {
				
				if (speakerPool.inactiveSpeakers.Count == 0) {
					
					speaker = GameObject.Instantiate (Resources.Load ("Prefabs/Effects/Speaker") as GameObject);

				} else {

					speaker = speakerPool.inactiveSpeakers.Dequeue () as GameObject;

				}

				speaker.SetActive (false);
				speaker.name = soundName + "_speaker";

				string suffix;
				int i = 0;
				while (speakerPool.deployedSpeakers.ContainsKey (speaker.name) ){
					suffix = "_" + i;
					speaker.name = soundName + "_speaker" + suffix;
					i++;
				}

				speakerPool.deployedSpeakers.Add (speaker.name, speaker);
			
				audioSource = speaker.GetComponent <AudioSource> ();
				audioSource.clip = soundLibrary [soundName];
				audioSource.Play ();
				speaker.SetActive (true);

			}
			speaker.transform.position = position;

			audioSource.volume = volume;

			return true;
		}
		return false;
	}

		

	void OnEnable(){
		GamePlayManagerScript.OnSetGameActiveEvent += LoadThemeMusic ;
		SpeakerScript.OnSoundPlayFinish += DeleteSpeaker;
		GamePlayManagerScript.OnGameRestartEvent += ResetSpeakers;

	}

	void OnDisable(){
		GamePlayManagerScript.OnSetGameActiveEvent -= LoadThemeMusic ;
		SpeakerScript.OnSoundPlayFinish += DeleteSpeaker;
		GamePlayManagerScript.OnGameRestartEvent -= ResetSpeakers;
	}


	void PlayThemeMusic (bool play){
		if (play)
			themeMusicSpeaker.Play ();
		else
			themeMusicSpeaker.Stop();
	}

	void DeleteSpeaker(string name) {
		
		if (speakerPool.deployedSpeakers.ContainsKey (name)) {

			speakerPool.inactiveSpeakers.Enqueue (speakerPool.deployedSpeakers [name]);
			speakerPool.deployedSpeakers.Remove (name);
			speakerPool.deployedSpeakers [name].SetActive (false);

		} else {
			Debug.Log ("SoundManagerScript: Can't find speaker of that name");
		}
	}

	void ResetSpeakers (){
		foreach (string objectName in speakerPool.deployedSpeakers.Keys) {
			DeleteSpeaker (objectName);
		}
	}
}
