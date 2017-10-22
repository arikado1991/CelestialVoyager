using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerScript : MonoBehaviour {

	public delegate void ApplyForce(Vector2 forceVector);
	public delegate void GetValue <T> (T delta);
	public delegate void SetValue <T> (T value);

	public delegate void GameDelegate ();



	public delegate void CollideWithObject ();



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
