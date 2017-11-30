using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlashingScreenScript : MonoBehaviour {

	Image screen;
	float alpha;
	float FADE_AMOUNT = .2f;
	float fadeRate;


	// Use this for initialization
	void Start () {
		screen = GetComponent<Image> ();
		Activate (false);
		OutOfBoundScript.OutOfBoundEvent += Activate;
	}

	void Activate (bool isActivate) {
		Debug.Log ("Flashing screen activation: " + isActivate);
		screen.enabled = isActivate;
		alpha = (isActivate) ? 0 : -10;
	}

	// Update is called once per frame
	void Update () {
		Color rendererColor = screen.color;

		if (alpha != -10) {
			Debug.Log ("Should be flashing");
			if (rendererColor.a <= 0) {
				
				rendererColor.a = 0;
				fadeRate = FADE_AMOUNT;
			} else if (rendererColor.a >= 1) {
				rendererColor.a = 1;
				fadeRate = -FADE_AMOUNT;
			}

			screen.color = new Color (
				rendererColor.r,
				rendererColor.b,
				rendererColor.g,
				rendererColor.a += fadeRate);
		}
	}
}
