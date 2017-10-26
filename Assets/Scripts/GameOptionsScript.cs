using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOptionsScript : MonoBehaviour {

	// Change value in here please

	public const short SHIP_MASS = 20;
	public const short MAX_FUEL_AMOUNT = 100;

	public const short FUEL_USAGE_MULTIPLIER = 6;
	public const short FORCE_MULTIPLIER = 1;

	public static short TIME_PENALTY_PER_SECOND = 2;


	public static Vector2 ASPECT_RATIO;
	public static Vector2 SCREEN_CENTER_POINT;

	public const short MIN_CAMERA_DISTANCE = 15;
	public const short MAX_CAMERA_DISTANCE = 25;


	public const int SCREEN_HEIGHT_IN_UNIT = 8;
	public static float UNIT_TO_PIXEL;




	public static float G_CONSTANT = 20f * Mathf.Pow (2, -26f);
	public static float MAX_VELOCITY = 3.0f;


	void Awake () {
		SCREEN_CENTER_POINT = new Vector2 (Screen.width * 0.5f, Screen.height * 0.5f);
		//Debug.Log ("Screen resolution is " + Screen.width + "," + Screen.height);

		ASPECT_RATIO = new Vector2 (16, 9); 

		UNIT_TO_PIXEL = Screen.height / SCREEN_HEIGHT_IN_UNIT;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
