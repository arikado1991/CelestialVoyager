using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelScoreSystemScript : MonoBehaviour {

	int p_endLevelScore = 0;
	public int starEarned;
	GamePlayManagerScript gameManager;

	void Start(){
		gameManager = GameObject.FindObjectOfType<GamePlayManagerScript> ();
	}

	public int endLvScore {
		get { GetEndLevelScore(); return p_endLevelScore; }
	}
	// Use this for initialization
	void OnEnable (){
		GamePlayManagerScript.OnEndLevelEvent  += GetEndLevelScore;
	
	}

	void OnDisable () {
		GamePlayManagerScript.OnEndLevelEvent  -= GetEndLevelScore;

	}

	void GetEndLevelScore(){
		
		LevelScript levelInfo = GetComponent<LevelScript> ();

		float remainingFuel = gameManager.player.GetComponent <SpaceshipInfoScript>().fuel;
		float completionTime = gameManager.timer.time;

		starEarned = 1
			+ ((remainingFuel > levelInfo.fuel2star && completionTime < levelInfo.time2star) ? 1 : 0)
			+ ((remainingFuel > levelInfo.fuel3star && completionTime < levelInfo.time3star) ? 1 : 0);

		p_endLevelScore = Mathf.Max (0,
			(int) (gameManager.player.GetComponent <SpaceshipInfoScript>().fuel * 10
				-	gameManager.timer.time * GameOptionsScript.TIME_PENALTY_PER_SECOND));
	}


}
