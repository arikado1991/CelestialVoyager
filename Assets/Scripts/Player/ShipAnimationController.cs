using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimationController : MonoBehaviour {

	public Sprite[] exhaustionType;
	SpriteRenderer flameRenderer;
	SpriteRenderer shipRenderer;
	// Use this for initialization
	void Start () {
		shipRenderer = GetComponent <SpriteRenderer> ();
		flameRenderer = transform.Find("ExhaustionFlame").GetComponentInChildren <SpriteRenderer>();
	}


	
	public void ChangeExhaustionFireAnimation (SpaceshipMovementScript.ExhaustionLevel exhaustionLevel){
		switch (exhaustionLevel) {
		case SpaceshipMovementScript.ExhaustionLevel.NONE:
			flameRenderer.enabled = false;
			break;
		default:
			flameRenderer.enabled = true;
			flameRenderer.sprite = exhaustionType [(int)exhaustionLevel - 1];
			break;
		}
	}


}
