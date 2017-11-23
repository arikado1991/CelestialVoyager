﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GamePlayManagerScript : MonoBehaviour {


	PopUpManagerScript popUpManager;

	public TimerScript timer;
	public EndLevelScoreSystemScript scoreSystem;
	public GameObject player;
	public LevelScript levelInfo;

	static GamePlayManagerScript s_gameplayMangager;
	public static GamePlayManagerScript GetInstance(){
		return s_gameplayMangager;
	}

	// related events
//	public static event EventManagerScript.GameDelegate OnGameOverEvent;
	public static event EventManagerScript.GameDelegate OnGameRestartEvent;
	public static event EventManagerScript.GameDelegate OnEndLevelEvent;
	public static event EventManagerScript.GameDelegate OnNewLevelLoaded;
	public static event EventManagerScript.GetValueDelegate <bool> OnSetGameActiveEvent;

	void Awake () {
		if (s_gameplayMangager != null && s_gameplayMangager != this) {
			GameObject.Destroy (this.gameObject);
			return;
		} 
		s_gameplayMangager = this;
		DontDestroyOnLoad (this.gameObject);

		SceneManager.sceneLoaded += AtNewLevel;

	}



	// Use this for initialization
	void AtNewLevel (Scene newScene, LoadSceneMode loadSceneMode) {
		OnNewLevelLoaded ();
		Debug.Log ("Current scene " + newScene.name + ", load mode: " + loadSceneMode);
		
		popUpManager = PopUpManagerScript.GetInstance ();
		//effectManager = GameObject.FindObjectOfType <GameEffectManagerScript> ();
		if (player == null)
			player = GameObject.FindGameObjectWithTag ("Player");

	

		PopUpScript popUp = popUpManager.CreatePopUp (PopUpManagerScript.PopUpType.MESSAGE, "GreetingPopUp").GetComponent<PopUpScript> ();
		popUp.SetDimension (1f, 1f);
		popUp.GetContent ("Title").GetComponent<Text>().text = "Welcome";
	
		popUp.GetContent ("Message").GetComponent<Text>().text = "Are you ready to explore the cosmos and beyond?";
		ButtonPanelScipt buttonPanel = popUp.GetContent("ButtonPanelPrefab").GetComponent <ButtonPanelScipt> ();
		buttonPanel.SetButton  (0, "Um ... no??", Restart);
		buttonPanel.EvenlyPlaceButton ();

		popUp = popUpManager.CreatePopUp (PopUpManagerScript.PopUpType.MESSAGE, "GameOverPopUp").GetComponent<PopUpScript> ();
		popUp.SetDimension (1f, 1f);
		popUp.GetContent ("Title").GetComponent<Text>().text = "Out of fuel!";
		popUp.GetContent ("Message").GetComponent<Text>().text = "You have been fired from NASA.";
		buttonPanel = popUp.GetContent("ButtonPanelPrefab").GetComponent <ButtonPanelScipt> ();
		buttonPanel.SetButton  (0, "Replay", Restart);
		buttonPanel.EvenlyPlaceButton ();

		popUp = popUpManager.CreatePopUp (PopUpManagerScript.PopUpType.ENDGAMERANKING, "EndLevelPopUp").GetComponent<PopUpScript> ();
		popUp.SetDimension (1f, 1f);

		popUp.GetContent ("Title").GetComponent<Text>().text = "Level completed";
		buttonPanel = popUp.GetContent("ButtonPanelPrefab").GetComponent <ButtonPanelScipt> ();
		buttonPanel.SetButton  (0, "Replay", Restart);
		buttonPanel.SetButton  (1, "Next level", LoadNextLevel);
		buttonPanel.EvenlyPlaceButton ();

		popUpManager.HideAllPopup ();

//	
		timer = GameObject.FindObjectOfType <TimerScript> ();
		scoreSystem = GameObject.FindObjectOfType <EndLevelScoreSystemScript> ();
		levelInfo = GameObject.FindObjectOfType <LevelScript>();
		BeginLevelBrief ();


	}

	void BeginLevelBrief () {
		ActivateGamePlay (false);
		popUpManager.ShowPopUp ("GreetingPopUp", true);
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
		//popUpManager.ShowPopUp ("GreetingPopUp", false);
		popUpManager.HideAllPopup();
		ActivateGamePlay (true);

	}

	public void GameOver() {
		ActivateGamePlay (false);
		popUpManager.ShowPopUp ("GameOverPopUp", true);


	}

	public void FinishLevel() {
		ActivateGamePlay (false);

		if (scoreSystem == null)
			scoreSystem = FindObjectOfType<EndLevelScoreSystemScript> ();
		PopUpScript popUp = popUpManager.GetPopUp ("EndLevelPopUp").GetComponent<PopUpScript> ();
		popUp.GetContent ("Message").GetComponent<Text> ().alignment = TextAnchor.MiddleLeft;
		popUp.GetContent ("Message").GetComponent<Text> ().text = 
			"Fuel: " + (int)player.GetComponent<SpaceshipInfoScript> ().fuel +
		"\nTime: " + timer.timeStr +
		"\nScore: " + scoreSystem.endLvScore;

		Image[] stars = popUp.transform.Find ("Stars").GetComponentsInChildren <Image>();

		for (int i = 0; i < 3; i++)

			stars [i].enabled = (i < scoreSystem.starEarned);

		popUpManager.ShowPopUp ("EndLevelPopUp", true);
	}

	public void LoadNextLevel() {
	//	Debug.Log ("I should load the next level. If there was one!");
	//	Debug.Log ("Current scene: " + SceneManager.GetActiveScene().buildIndex + ", next scene: " + SceneManager.sceneCount);
		SceneManager.LoadScene ((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);


	}
}
