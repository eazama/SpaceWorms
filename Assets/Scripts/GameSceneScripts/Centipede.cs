using UnityEngine;
using System.Collections;

public class Centipede : MonoBehaviour {

	public int speed = 300;

	public string origin = "top";
	public string direction = "right";
	public CentipedeBody nextSegment;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Barrier" || col.gameObject.tag == "Centipede") {
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
			Destroy (gameObject);
			Destroy (col.gameObject);
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
		Vector3 pos = gameObject.rigidbody.position;
		Vector3 newPos = gameObject.rigidbody.position;
		switch (origin) {
			case "top":
				newPos += Vector3.down * gameObject.transform.localScale.x;
				break;
			case "bottom":
				newPos += Vector3.up * gameObject.transform.localScale.x;
				break;
			case "left":
				newPos += Vector3.right * gameObject.transform.localScale.x;
				break;
			case "right":
				newPos += Vector3.left * gameObject.transform.localScale.x;
				break;
		}
		switch (direction) {//added rotation to this and moved the block up a line to make it rotate 90 degrees on time
		case "up":
			direction = "down";
			break;
		case "down":
			direction = "up";
			break;
		case "left":
			direction = "right";
			transform.Rotate (Vector3.forward, 90);
			break;
		case "right":
			direction = "left";
			transform.Rotate (Vector3.forward, -90);
			break;
		}
		yield return StartCoroutine( Move(pos, newPos));
		switch (direction) { //rotates it a second time to line it up with the direction it's going
		case "up":
			break;
		case "down":
			break;
		case "left":
			transform.Rotate (Vector3.forward, -90);
			break;
		case "right":
			transform.Rotate (Vector3.forward, 90);
			break;
		}
		yield return StartCoroutine (MoveOnceDirection (direction));
		StartCoroutine (MoveDirection(direction));
	}

	IEnumerator MoveDirection(string direction){
		while (true) {
			yield return StartCoroutine(MoveOnceDirection(direction));
		}
	}

	IEnumerator MoveOnceDirection(string direction){
		Vector3 pos = gameObject.rigidbody.position;
		Vector3 newPos = gameObject.rigidbody.position;
		switch (direction) {
		case "down":
			newPos += Vector3.down * gameObject.transform.localScale.x;
			break;
		case "up":
			newPos += Vector3.up * gameObject.transform.localScale.x;
			break;
		case "right":
			newPos += Vector3.right * gameObject.transform.localScale.x;
			break;
		case "left":
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
}
