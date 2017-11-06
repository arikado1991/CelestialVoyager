using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GamePlayManagerScript : MonoBehaviour {


	PopUpManagerScript popUpManager;

	public TimerScript timer;
	public EndLevelScoreSystemScript scoreSystem;
	public GameObject player;


	static GamePlayManagerScript s_gameplayMangager;
	public static GamePlayManagerScript GetInstance(){
		return s_gameplayMangager;
	}

	// related events
//	public static event EventManagerScript.GameDelegate OnGameOverEvent;
	public static event EventManagerScript.GameDelegate OnGameRestartEvent;
	public static event EventManagerScript.GameDelegate OnEndLevelEvent;
	public static event EventManagerScript.GetValueDelegate <bool> OnSetGameActiveEvent;

	void Awake () {
		if (s_gameplayMangager != null && s_gameplayMangager != this) {
			GameObject.Destroy (this.gameObject);
			return;
		} 
		s_gameplayMangager = this;
		DontDestroyOnLoad (this.gameObject);

	}

	// Use this for initialization
	void Start () {

		popUpManager = PopUpManagerScript.GetInstance ();
		//effectManager = GameObject.FindObjectOfType <GameEffectManagerScript> ();
		player = GameObject.FindGameObjectWithTag ("Player");

		popUpManager.ClearPopup ();
		popUpManager.SetPopUp ("Welcome to Space Voyager!", "Are you ready to explore the cosmos and beyond?");
		popUpManager.SetButtonFunction (0, "Um .. no?", Restart);
		popUpManager.ShowPopUp (true);

//		Debug.Log ("Show pop up at the beginning");
//		Debug.Log (player != null);
		timer = GameObject.FindObjectOfType <TimerScript> ();
		scoreSystem = GameObject.FindObjectOfType <EndLevelScoreSystemScript> ();

		Restart ();

	}



	void OnEnable(){
		SpaceshipInfoScript.OnFuelEmptyEvent += GameOver;
		FinishedPlanetScript.OnReachingDestinationEvent += FinishLevel;

	}

	void OnDisable() {
		SpaceshipInfoScript.OnFuelEmptyEvent -= GameOver;
		FinishedPlanetScript.OnReachingDestinationEvent -= FinishLevel;
	}



	void ActivateGamePlay(bool is_activated) {
		
		player.SetActive (is_activated);
		OnSetGameActiveEvent (is_activated);

	}

	public void Restart(){
		//Debug.Log ("GamePLayManage: Hit restart");
		OnGameRestartEvent ();
		popUpManager.ShowPopUp (false);
		ActivateGamePlay (true);

	}

	public void GameOver() {
		ActivateGamePlay (false);
		popUpManager.ClearPopup ();
		popUpManager.SetPopUp ("Game Over!", 
			"You are out of fuel!\nYou're now a cold corpse that wanders the universe for all eternity!");
		
		popUpManager.SetButtonFunction (0, "Replay", Restart);
		popUpManager.ShowPopUp (true);

	}

	public void FinishLevel() {
		ActivateGamePlay (false);
		OnEndLevelEvent ();
		popUpManager.ClearPopup ();
		popUpManager.SetPopUp (
			"Level finished!", 

			"Fuel: " + (int)player.GetComponent<SpaceshipInfoScript> ().fuel + 
				" lt\nTime: " +  timer.timeStr + 
				"\nScore: " + scoreSystem.endLvScore);
		
		popUpManager.SetButtonFunction (0, "Replay", Restart);
		popUpManager.SetButtonFunction (1, "Continue", LoadNextLevel);
		popUpManager.ShowPopUp (true);
	}

	public void LoadNextLevel() {
		Debug.Log ("I should load the next level. If there was one!");
		SceneManager.LoadScene ("TestScene2");
		Restart ();


	}
}
