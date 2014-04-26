using UnityEngine;
using System.Collections;

public class Centipede : MonoBehaviour {

	public int speed = 300;

	public string origin = "top";
	public string direction = "right";
	public CentipedeBody nextSegment;
	protected AudioClip deathSound;
	public bool dying = false; //prevents rapidfire shots from spawning multiple asteroids from a single segment
	public float AtmDrainSpeed = .5f; //Subtract one atmosphere health every X seconds
	protected bool isFeeding = false;
	protected Color currentColor;
	public Animator wormEats;
	private bool reversing = false;

	public GameController gameController;

	// Use this for initialization
	void Start () {
		deathSound = Resources.Load<AudioClip> ("Centipede_death");
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		audio.enabled = true;
		if (nextSegment != null) {
			nextSegment.origin = origin;
			nextSegment.direction = direction;
		}

		if (gameController.areaNumber % 5 == 1) {//green
			gameObject.renderer.material.color = new Color(.1f, 1, .1f);
		}
		else if (gameController.areaNumber % 5 == 2) {//pink
			gameObject.renderer.material.color = new Color(.973f, .153f, .984f);
		}
		else if (gameController.areaNumber % 5 == 3) {//red
			gameObject.renderer.material.color = new Color(1, .1f, .1f);
		}
		else if (gameController.areaNumber % 5 == 4) {///blue
			gameObject.renderer.material.color = new Color(0,.851f, .965f);
		}
		else {//white
			gameObject.renderer.material.color = new Color(1,1,1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (gameController.isGameOver) {
			StopAllCoroutines ();
			return;
		}
		if (nextSegment == null) {
			speed = 550;
		}
		if (transform.position.x > 416 || transform.position.x < -416 || 
				transform.position.y > 416 || transform.position.y < -416) {
			if(nextSegment != null){
				makeNextSegmentHead ();
			}
			Destroy(gameObject);
		}
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
		if (reversing == true && col.tag == "Asteroid") {
			Destroy (col.gameObject);
			return;
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
			gameController.AddScore(100);
		}
		if (col.tag == "Planet") {
			stopMove();
			stopWorm();
			isFeeding = true;
			wormEats.SetBool ("eatAtmosphere", true);
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
		reversing = true;
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
			if (gameObject.transform.position.x >= 300) {
				direction = "left";
			} else if (gameObject.transform.position.x <= -300) {
				direction = "right";
			} else {
				if( direction == "left"){
					direction = "right";
				} else if (direction == "right"){
					direction = "left";
				}
			}
		} else {
			if (gameObject.transform.position.y >= 300) {
				direction = "down";
			} else if (gameObject.transform.position.y <= -300) {
				direction = "up";
			} else{
				if(direction == "up"){
					direction = "down";
				} else if(direction == "down"){
					direction = "up";
				}
			}
		}
		yield return StartCoroutine (MoveOnceDirection (direction));
		StartCoroutine (MoveDirection(direction));
		reversing = false;
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
			gameObject.transform.eulerAngles = new Vector3(0,0,180);
			newPos += Vector3.down * gameObject.transform.localScale.x;
			break;
		case "up":
			gameObject.transform.eulerAngles = new Vector3(0,0,0);
			newPos += Vector3.up * gameObject.transform.localScale.x;
			break;
		case "right":
			gameObject.transform.eulerAngles = new Vector3(0,0,-90);
			newPos += Vector3.right * gameObject.transform.localScale.x;
			break;
		case "left":
			gameObject.transform.eulerAngles = new Vector3(0,0,90);
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
		Instantiate (Resources.Load("Asteroid"), new Vector3(Mathf.Round ((transform.position.x/32))*32,
		                                   Mathf.Round ((transform.position.y/32))*32,
		                                   Mathf.Round ((transform.position.z/32))*32),
		                             Quaternion.identity);
	}
	
	public void makeNextSegmentHead(){
		GameObject oldHead = nextSegment.gameObject;
		GameObject newHead = Instantiate (Resources.Load ("Centipede head"), oldHead.transform.position,new Quaternion()) as GameObject;
		Centipede cen = newHead.GetComponent<Centipede> ();
		cen.origin = origin;
		cen.direction = direction;
		cen.StartCoroutine(cen.MoveDirection(cen.direction));
		cen.nextSegment = oldHead.GetComponent<Centipede> ().nextSegment;
		//cen.deathSound = oldHead.GetComponent<Centipede> ().deathSound;
		Destroy (oldHead);
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

	public void addSegment(int segments){
		Centipede lastSeg = this;
		CentipedeBody newSeg;
		while(lastSeg.nextSegment!=null){
			lastSeg = lastSeg.nextSegment;
		}
		for(int i = 0; i < segments; ++i){
			GameObject Seg = Instantiate(Resources.Load("Centipede body"), lastSeg.transform.position, new Quaternion()) as GameObject;
			newSeg = Seg.GetComponent<CentipedeBody>();
			newSeg.origin = lastSeg.origin;
			newSeg.direction = lastSeg.direction;
			lastSeg.nextSegment = newSeg;
			lastSeg = lastSeg.nextSegment;
		}
	}
}
