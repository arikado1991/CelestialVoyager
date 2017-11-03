using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerScript : MonoBehaviour {

	public delegate void GetValueDelegate <T> (T delta);
	public delegate void MultipleParameterDelegate (int size, params object[] param);
	public delegate void GameDelegate ();
}
