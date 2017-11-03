using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEffectManagerScript : MonoBehaviour {

	Dictionary <string, GameObject> effectLibrary;

	public static event EventManagerScript.MultipleParameterDelegate OnCreateEffectEvent;

	void Awake() {
		effectLibrary = new Dictionary <string, GameObject> ();
		Object[] tempEffectArray = Resources.LoadAll ("Prefabs/Effects");
		foreach (Object o in tempEffectArray) {
			effectLibrary.Add (o.name, o as GameObject);
			Debug.Log (o.name);
		}
	}
	// Use this for initialization
	void OnEnable() {
		
		OnCreateEffectEvent += CreateEffect;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void CreateEffect (int parameterCount, object[] parameters) {

		string effectName = parameters [0] as string;
		Vector2 position = (Vector2) parameters [1] ;
		float duration = (parameterCount > 2) ? ((float) parameters [2]) : 0;


		GameObject effect = GameObject.Instantiate (effectLibrary[effectName]);
		effect.transform.position = new Vector3( position.x, position.y, -0.1f);
		GameObject.Destroy (effect, (duration != 0) ? duration : effect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
	}
}
