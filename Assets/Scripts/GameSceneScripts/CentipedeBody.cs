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
			Destroy(gameObject, deathSound.length);
		}
	}

	public void startMove(Vector3 from, Vector3 to){
		StartCoroutine(Move (from, to));
		if (nextSegment != null) {
			nextSegment.startMove (nextSegment.rigidbody.position, gameObject.rigidbody.position);
		}
	}

	public void stopMove(){
		StopAllCoroutines ();
		if (nextSegment != null) {
			nextSegment.stopMove ();
		}
	}

}
