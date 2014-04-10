using UnityEngine;
using System.Collections;

public class CentipedeBody : MonoBehaviour {

	public int speed = 300;
	public CentipedeBody nextSegment = null;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Bullet") {
			Destroy (gameObject);
			Destroy (col.gameObject);
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

	public IEnumerator Move(Vector3 from, Vector3 to){
		float startTime = Time.time;
		float dist = Vector3.Distance(from, to);
		while(gameObject.rigidbody.position != to){
			float timePassed = (Time.time - startTime)*speed;
			gameObject.rigidbody.position = Vector3.Lerp (from, to, timePassed/dist);
			yield return null;
		}
		if (nextSegment != null) {
			nextSegment.startMove(from, to);
		}
	}
}
