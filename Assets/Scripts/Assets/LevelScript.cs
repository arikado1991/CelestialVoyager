using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour
{

	// Use this for initialization
	public int levelIndex;
	public string LevelName;

	public float maxFuel;
	public float maxTime;
	public Vector2 startingPosition;

	//3 star condition:
	public float fuel3star;
	public float fuel2star;
	public float time3star;
	public float time2star;
}

