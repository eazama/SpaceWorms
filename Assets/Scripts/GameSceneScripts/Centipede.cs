using UnityEngine;
using System.Collections;

public class Centipede : MonoBehaviour {

	public int speed = 300;

	public string origin = "top";
	public string direction = "right";
	public CentipedeBody nextSegment;
	public Transform asteroid;
	public Sprite HeadSprite;
	public AudioClip deathSound;
	protected bool dying = false; //prevents rapidfire shots from spawning multiple asteroids from a single segment
	public float AtmDrainSpeed = .5f; //Subtract one atmosphere health every X seconds
	protected bool isFeeding = false;

	// Use this for initialization
	void Start () {
		audio.enabled = true;
		if (nextSegment != null) {
			nextSegment.origin = origin;
			nextSegment.direction = direction;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider col){
		if (origin == "bottom" && col.tag == "BarrierTop") {
			origin = "top";
			direction = "right";
		} else if (origin == "top" && col.tag == "BarrierBottom") {
			origin = "bottom";
			direction = "left";
		} else if (origin == "left" && col.tag == "BarrierRight") {
			origin = "left";
			direction = "up";
		} else if (origin == "right" && col.tag == "BarrierLeft") {
			origin = "right";
			direction = "down";
		}

		if (col.tag.Contains("Barrier") || col.tag == "Centipede" || col.tag == "Asteroid") {
			CentipedeBody centi= col.gameObject.GetComponent<CentipedeBody>();
			if(centi != null && isBodySegment(centi)){
				return;
			}
			if(isFeeding){
				return;
			}
			StopAllCoroutines();
			if (nextSegment != null) {
				nextSegment.startMove(nextSegment.rigidbody.position, gameObject.rigidbody.position);
			}
			StartCoroutine(ReverseDirection());
		}
		if (col.tag == "Bullet" &&!dying) {
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
		if (col.tag == "Planet") {
			stopMove ();
			stopWorm();
			isFeeding = true;
			StartCoroutine (drainHealth(AtmDrainSpeed));
		}
	}

	public IEnumerator drainHealth(float interval){
		while (true) {
			GameObject.Find ("Atmosphere").GetComponent<Atmosphere> ().subtractHealth ();
			yield return new WaitForSeconds (interval);
		}
	}

	public IEnumerator Move(Vector3 from, Vector3 to){
		if (from.Equals (to)) {
			yield break;
		}
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

	IEnumerator ReverseDirection(){
		stopWorm ();
		restartWorm ();
		switch (origin) {
		case "top":
			yield return StartCoroutine (MoveOnceDirection ("down"));
			break;
		case "bottom":
			yield return StartCoroutine (MoveOnceDirection ("up"));
			break;
		case "left":
			yield return StartCoroutine (MoveOnceDirection ("right"));
			break;
		case "right":
			yield return StartCoroutine (MoveOnceDirection ("left"));
			break;
		}

		if (origin == "top" || origin == "bottom") {
			if (rigidbody.position.x >= 0) {
				direction = "left";
			} else {
				direction = "right";
			}
		} else {
			if (rigidbody.position.y >= 0) {
				direction = "down";
			} else {
				direction = "up";
			}
		}
		yield return StartCoroutine (MoveOnceDirection (direction));
		StartCoroutine (MoveDirection(direction));
	}

	public IEnumerator MoveDirection(string direction){
		while (true) {
			yield return StartCoroutine(MoveOnceDirection(direction));
		}
	}

	IEnumerator MoveOnceDirection(string direction){

		Vector3 pos = gameObject.rigidbody.position;
		Vector3 newPos = gameObject.rigidbody.position;
		switch (direction) {
		case "down":
			transform.eulerAngles = new Vector3(0,0,180);
			newPos += Vector3.down * gameObject.transform.localScale.x;
			break;
		case "up":
			transform.eulerAngles = new Vector3(0,0,0);
			newPos += Vector3.up * gameObject.transform.localScale.x;
			break;
		case "right":
			transform.eulerAngles = new Vector3(0,0,-90);
			newPos += Vector3.right * gameObject.transform.localScale.x;
			break;
		case "left":
			transform.eulerAngles = new Vector3(0,0,90);
			newPos += Vector3.left * gameObject.transform.localScale.x;
			break;
		}
		newPos.x = Mathf.Round (newPos.x / 32) * 32;
		newPos.y = Mathf.Round (newPos.y / 32) * 32;
		yield return StartCoroutine(Move(pos, newPos));
	}

	bool isBodySegment(CentipedeBody check){
		CentipedeBody cb = nextSegment;
		while(cb != null){
			if(check == cb){
				return true;
			}
			cb = cb.nextSegment;
		}
		return false;
	}

	public void DropAsteroid(){
		Instantiate (asteroid, new Vector3(Mathf.Round ((transform.position.x/32))*32,
		                                   Mathf.Round ((transform.position.y/32))*32,
		                                   Mathf.Round ((transform.position.z/32))*32),
		                             Quaternion.identity);
	}
	
	public void makeNextSegmentHead(){
		nextSegment.StopAllCoroutines();
		GameObject seg = nextSegment.gameObject;
		SpriteRenderer sr = seg.GetComponent<SpriteRenderer>() as SpriteRenderer;
		sr.sprite = HeadSprite;
		Centipede segScript = seg.AddComponent<Centipede>();
		CentipedeBody bodySeg = seg.GetComponent<CentipedeBody> ();
		segScript.nextSegment = bodySeg.nextSegment;
		segScript.HeadSprite = HeadSprite;
		segScript.asteroid = asteroid;
		segScript.deathSound = deathSound;
		Destroy(bodySeg);
		segScript.StartCoroutine(segScript.MoveDirection(segScript.direction));
		//if (segScript.nextSegment != null) {
		//	segScript.nextSegment.startMove(segScript.nextSegment.transform.position, segScript.transform.position);
		//}
		segScript.restartWorm ();
	}

	public void startMove(Vector3 from, Vector3 to){
		StartCoroutine(Move (from, to));
	}
	
	public void stopMove(){
		StopAllCoroutines ();
	}

	public void stopWorm(){
		//StopAllCoroutines ();
		CentipedeBody ns = nextSegment;
		while (ns != null) {
			ns.StopAllCoroutines();
			ns = ns.nextSegment;
		}
	}
	public void restartWorm(){
		Centipede ns = this;
		while (ns.nextSegment != null) {
			ns.nextSegment.startMove(ns.nextSegment.rigidbody.position, ns.rigidbody.position);
			ns = ns.nextSegment;
		}
	}
}
