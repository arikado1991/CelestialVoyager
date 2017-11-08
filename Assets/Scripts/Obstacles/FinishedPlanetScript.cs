using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedPlanetScript : MonoBehaviour {

	public static event EventManagerScript.GameDelegate OnReachingDestinationEvent;
	GameObject guideArrow;

	void Start(){
		guideArrow = GameObject.Instantiate (Resources.Load ("Prefabs/UI/GuideArrowPrefab") as GameObject, transform);
		guideArrow.GetComponent<GuidingArrowScript> ().Ready ();

		GamePlayManagerScript.OnSetGameActiveEvent += CastGuideArrow;
	

	}
	// Use this for initialization
	void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.CompareTag ("Player"))
			OnReachingDestinationEvent ();
	}

	void CastGuideArrow (bool visible) {
		Debug.Log ("Cast arrow : " + visible);
		if (guideArrow != null)
			guideArrow.SetActive (visible);
	}
}
