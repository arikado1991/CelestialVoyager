using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOptionsScript : MonoBehaviour {

	// Change value in here please

	public float GRAVITATIONAL_MULTIPLIER = .5f;


	public static Vector2 ASPECT_RATIO;
	public static Vector2 SCREEN_CENTER_POINT;
	public static float WATER_LEVEL;
	public static float MOVEMENT_SCALE;


	public const int SCREEN_HEIGHT_IN_UNIT = 8;
	public static float UNIT_TO_PIXEL;
	public const float GAME_DURATION = 60.0f;


	static public float MAX_FUEL_AMOUNT = 100.0f;

	public static float G_CONSTANT = 20f * Mathf.Pow (2, -26f);
	public static float MAX_VELOCITY = 3.0f;



	public Sprite[] NUMBER_SPRITE;

	void Awake () {
		SCREEN_CENTER_POINT = new Vector2 (Screen.width * 0.5f, Screen.height * 0.5f);
		Debug.Log ("Screen resolution is " + Screen.width + "," + Screen.height);

		ASPECT_RATIO = new Vector2 (16, 9); 

		MOVEMENT_SCALE = Screen.height / ASPECT_RATIO.y;
		WATER_LEVEL = Screen.height / ASPECT_RATIO.y * 8;

		UNIT_TO_PIXEL = Screen.height / SCREEN_HEIGHT_IN_UNIT;


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Sprite GetNumber (short number){
		return NUMBER_SPRITE [number];

	}
}
