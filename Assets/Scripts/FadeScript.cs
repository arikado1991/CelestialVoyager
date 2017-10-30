using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour {

	public float fadeDuration ;
	float fadeAmount;


	private SpriteRenderer[] spriteRenderers;
	private Image[] images;

	bool destroyAfterFaded;

	public void ActivateFading(float duration, float alpha) {
		
		fadeDuration = duration;
		fadeAmount = alpha / fadeDuration;
		if (spriteRenderers != null) {
			foreach (SpriteRenderer renderer in spriteRenderers) {

				renderer.color = new Color (
					renderer.color.r,
					renderer.color.g,
					renderer.color.b,
					alpha);

			}
		}
		if (images != null) {
			foreach (Image image in images) {

				image.color = new Color (
					image.color.r,
					image.color.g,
					image.color.b,
					alpha);
			}

		}


	}
	
	// Use this for initialization
	void Start () {
		spriteRenderers = GetComponentsInChildren<SpriteRenderer> ();
		images = GetComponentsInChildren<Image> ();
		if (spriteRenderers == null) {
			Debug.Log ("FadeScript: Sprite renderer not found.");
		}
		ActivateFading (0, 0);

	}


	
	// Update is called once per frame
	void Update () {
		if (fadeDuration > 0 ) {
			fadeDuration  -= Time.deltaTime;
			if (spriteRenderers != null) {
				foreach (SpriteRenderer renderer in spriteRenderers) {

					renderer.color = new Color (
						renderer.color.r,
						renderer.color.g,
						renderer.color.b,
						renderer.color.a - Time.deltaTime * fadeAmount);
				
				}
			}
			if (images != null) {
				foreach (Image image in images) {

					image.color = new Color (
						image.color.r,
						image.color.g,
						image.color.b,
						image.color.a - Time.deltaTime * fadeAmount);
				}

			}
		} 

	}
}
