using UnityEngine;
using System.Collections;

public class CentipedeBody : Centipede {

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
