using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public Sprite size2;
	public Sprite size3;
	public Sprite size4;
	public int health = 4;
	public AudioClip warningBeep;
	private GameController gameController;
	public Animator astExplode;
	public AudioClip deathSound;

	// Use this for initialization
	void Start () {
		astExplode = GetComponent<Animator> ();
		gameObject.transform.eulerAngles = new Vector3(0,0,Random.Range (0,360));
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null) {
			Debug.Log("Cannot find 'GameController' script");
		}
		if (transform.position.x >= -96 && transform.position.x <= 96 &&
		    transform.position.y >= -96 && transform.position.y <= 96) {
			StartCoroutine(explosionCountdown());
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
		if (col.gameObject.tag == "Bullet") {
			col.gameObject.SetActive(false);
			health--;
			switch(health){
			case 3:
				astExplode.SetBool("size2", true);
				break;
			case 2:
				astExplode.SetBool("size3", true);
				break;
			case 1:
				astExplode.SetBool("size4", true);
				break;
			}
			if(health <=0){
				gameController.AddScore(1);
				Destroy (gameObject);
			}
		}
	}

	IEnumerator explosionCountdown()
	{
		yield return new WaitForSeconds (1);
		for (int timer = 0; timer < 3; timer++) {
				//change to red, play warning
			Color c = gameObject.GetComponent<Renderer>().material.color;
			c.g = (0);
			c.b = (0);
			gameObject.GetComponent<Renderer>().material.color = c;
			GetComponent<AudioSource>().PlayOneShot(warningBeep);
			yield return new WaitForSeconds (0.25f);
				//change to normal
			c.g = (1);
			c.b = (1);
			gameObject.GetComponent<Renderer>().material.color = c;
			yield return new WaitForSeconds (1);
		}
		explosion ();

	}

	void explosion(){
		if (health == 4) {
			astExplode.SetBool("explode", true);
		}
		else if (health == 3) {
			astExplode.SetBool("size2", true);
			astExplode.SetBool("explode", true);
		}
		else if (health == 2) {
			astExplode.SetBool("size3", true);
			astExplode.SetBool("explode", true);
		}
		else if (health == 1) {
			astExplode.SetBool("size4", true);
			astExplode.SetBool("explode", true);
		}
	}

	void asteroidDestroy() {
		GetComponent<AudioSource>().PlayOneShot (deathSound);
		Destroy (gameObject);
		Debug.Log ("asteroid explodes");
		GameObject.Find ("Atmosphere").GetComponent<Atmosphere> ().health -= 9;
		GameObject.Find ("Atmosphere").GetComponent<Atmosphere> ().subtractHealth ();
	}


}
