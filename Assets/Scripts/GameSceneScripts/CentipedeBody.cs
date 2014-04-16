using UnityEngine;
using System.Collections;

public class CentipedeBody : Centipede {

	// Use this for initialization
	void Start () {
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
			GameController.segmentsOut--;
			Destroy(gameObject, deathSound.length);
		}
	}
}
