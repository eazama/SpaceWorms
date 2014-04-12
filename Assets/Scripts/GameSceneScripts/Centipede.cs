using UnityEngine;
using System.Collections;

public class Centipede : MonoBehaviour {

	public int speed = 300;

	public string origin = "top";
	public string direction = "right";
	public CentipedeBody nextSegment;
	public Transform asteroid;
	public Sprite HeadSprite;

	// Use this for initialization
	void Start () {
		if (nextSegment != null) {
			nextSegment.origin = origin;
			nextSegment.direction = direction;
		}
		//StartCoroutine(ReverseDirection());
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Barrier" || col.gameObject.tag == "Centipede" || col.gameObject.tag == "Asteroid") {
			CentipedeBody centi= col.gameObject.GetComponent<CentipedeBody>();
			if(centi != null && isBodySegment(centi)){
				return;
			}
			StopAllCoroutines();
			if (nextSegment != null) {
				nextSegment.stopMove();
				nextSegment.startMove(nextSegment.rigidbody.position, gameObject.rigidbody.position);
			}
			StartCoroutine(ReverseDirection());
		}
		if (col.gameObject.tag == "Bullet") {
			Destroy (col.gameObject);
			DropAsteroid ();
			if(nextSegment != null){
				makeNextSegmentHead ();
			}
			Destroy (gameObject);
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

	IEnumerator ReverseDirection(){
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
		switch (direction) {
		case "up":
			direction = "down";
			break;
		case "down":
			direction = "up";
			break;
		case "left":
			direction = "right";
			break;
		case "right":
			direction = "left";
			break;
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
		Transform ast = Instantiate (asteroid, new Vector3(Mathf.Round ((transform.position.x/32))*32,
		                                                   Mathf.Round ((transform.position.y/32))*32,
		                                                   Mathf.Round ((transform.position.z/32))*32),
		                             Quaternion.identity) as Transform;
		
		ast.transform.eulerAngles = new Vector3(0,0,Random.Range (0,360));
	}
	
	public void makeNextSegmentHead(){
		nextSegment.StopAllCoroutines();
		GameObject seg = nextSegment.gameObject;
		SpriteRenderer sr = seg.GetComponent<SpriteRenderer>() as SpriteRenderer;
		sr.sprite = HeadSprite;
		Centipede segScript = seg.AddComponent<Centipede>();
		CentipedeBody bodySeg = seg.GetComponent<CentipedeBody> ();
		segScript.nextSegment = bodySeg.nextSegment;
		segScript.asteroid = asteroid;
		Destroy(bodySeg);
		segScript.StartCoroutine(segScript.MoveDirection(segScript.direction));
		if (segScript.nextSegment != null) {
			segScript.nextSegment.startMove(segScript.nextSegment.transform.position, segScript.transform.position);
		}
	}
}
