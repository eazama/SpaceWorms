using UnityEngine;
using System.Collections;

public class CentipedeBody : Centipede {

	//public GameController gameController;
	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
	}
	
	// Update is called once per frame
	void Update () {
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
			Destroy(gameObject, deathSound.length);
			gameController.AddScore(10);
		}
	}
}
