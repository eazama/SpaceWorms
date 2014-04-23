using UnityEngine;
using System.Collections;

public class CentipedeBody : Centipede {
	void Start () {
		//wormEats = GetComponent<Animator> ();
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController.areaNumber % 5 == 1) {//green
			currentColor = gameObject.renderer.material.color;
			currentColor.r = 0.1f;
			currentColor.g = 1;
			currentColor.b = 0.1f;
			gameObject.renderer.material.color = currentColor;
		}
		else if (gameController.areaNumber % 5 == 2) {//pink
			currentColor = gameObject.renderer.material.color;
			currentColor.r = 0.973f;
			currentColor.g = 0.153f;
			currentColor.b = 0.984f;
			gameObject.renderer.material.color = currentColor;
		}
		else if (gameController.areaNumber % 5 == 3) {//red
			currentColor = gameObject.renderer.material.color;
			currentColor.r = 1;
			currentColor.g = 0.1f;
			currentColor.b = 0.1f;
			gameObject.renderer.material.color = currentColor;
		}
		else if (gameController.areaNumber % 5 == 4) {///blue
			currentColor = gameObject.renderer.material.color;
			currentColor.r = 0;
			currentColor.g = 0.851f;
			currentColor.b = 0.965f;
			gameObject.renderer.material.color = currentColor;
		}
		else {//white
			currentColor = gameObject.renderer.material.color;
			currentColor.r = 1;
			currentColor.g = 1;
			currentColor.b = 1;
			gameObject.renderer.material.color = currentColor;
		}
	}

	// Update is called once per frame
	void Update () {
		if (gameController.isGameOver) {
			StopAllCoroutines ();
			return;
		}
	}
	
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Bullet" && !dying) {
			dying = true;
			col.gameObject.SetActive(false);
			audio.PlayOneShot(deathSound);
			renderer.enabled = false;
			DropAsteroid ();
			if(nextSegment != null){
				makeNextSegmentHead ();
			}
			Destroy(gameObject, 0.193f);
			gameController.AddScore(10);
		}
	}
}
