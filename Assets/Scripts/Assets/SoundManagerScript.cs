using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {
	Dictionary <string, AudioClip> musicLibrary;
	Dictionary <string, AudioClip> soundLibrary;

	public AudioClip gameTheme;
	public AudioSource themeMusicSpeaker;
	public AudioSource soundMaker;
	// Use this for initialization
	void Awake () {

		themeMusicSpeaker = transform.Find ("ThemeMusicPlayer").transform.GetComponent<AudioSource> ();
		soundMaker = transform.Find ("SoundMaker").transform.GetComponent <AudioSource> ();
		musicLibrary = new Dictionary <string, AudioClip> ();
		Object[] tempObjArray =  Resources.LoadAll ("Sounds/Music", typeof (AudioClip));
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

		if (soundMaker == null)
			soundMaker = GameObject.FindObjectOfType<GamePlayManagerScript>().player.GetComponent <AudioSource> ();
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

	void PlayDeadSound () {
		//soundMaker.clip = soundLibrary ["Explosion"];
		//soundMaker.PlayDelayed (1);
		soundMaker.clip = soundLibrary ["Wilhelm Scream"];
		soundMaker.Play ();

	}

	void PlayRocketFlyingSound (SpaceshipMovementScript.ExhaustionLevel exhaustionLevel){
		soundMaker.clip = soundLibrary ["Rocket Flying"];
		if (!soundMaker.isPlaying)
			soundMaker.Play ();
		switch (exhaustionLevel) {
		case SpaceshipMovementScript.ExhaustionLevel.NONE:
			soundMaker.Stop ();
			break;
		case SpaceshipMovementScript.ExhaustionLevel.SMALL:
			soundMaker.volume = 0.5f;
			break;
		case SpaceshipMovementScript.ExhaustionLevel.LARGE:
			soundMaker.volume = 1f;
			break;
		default:
			break;
		}

	}

	void OnEnable(){
		GamePlayManagerScript.OnSetGameActiveEvent += LoadThemeMusic ;
		PlanetGravityScript.OnSpaceshipCollisionWithPlanetEvent += PlayDeadSound;
		SpaceshipMovementScript.OnChangeExhaustion += PlayRocketFlyingSound;
	}

	void OnDisable(){
		GamePlayManagerScript.OnSetGameActiveEvent -= LoadThemeMusic ;
		PlanetGravityScript.OnSpaceshipCollisionWithPlanetEvent -= PlayDeadSound;
		SpaceshipMovementScript.OnChangeExhaustion -= PlayRocketFlyingSound;
	}
	// Update is called once per frame
	void Update () {
		
		
	}

	void PlayThemeMusic (bool play){
		if (play)
			themeMusicSpeaker.Play ();
		else
			themeMusicSpeaker.Stop();
	}


}
