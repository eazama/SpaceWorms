using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public Sprite size2;
	public Sprite size3;
	public Sprite size4;
	public int health = 4;
	public AudioClip warningBeep;
	private GameController gameController;

	// Use this for initialization
	void Start () {
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
		
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Bullet") {
			col.gameObject.SetActive(false);
			health--;
			SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
			switch(health){
			case 3:
				sr.sprite = size2;
				break;
			case 2:
				sr.sprite = size3;
				break;
			case 1:
				sr.sprite = size4;
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
			Color c = gameObject.renderer.material.color;
			c.g = (0);
			c.b = (0);
			gameObject.renderer.material.color = c;
			audio.PlayOneShot(warningBeep);
			yield return new WaitForSeconds (0.25f);
				//change to normal
			c.g = (1);
			c.b = (1);
			gameObject.renderer.material.color = c;
			yield return new WaitForSeconds (1);
		}
		//destroy asteroid, take 10 health from planet, play deathsound and explosion animation
		Destroy (gameObject);
		GameObject.Find ("Atmosphere").GetComponent<Atmosphere> ().health -= 9;
		GameObject.Find ("Atmosphere").GetComponent<Atmosphere> ().subtractHealth ();

	}
}
