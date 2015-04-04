using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public int playerSpeed = 5;
	public float leftClamp = -4.5f;
	public float rightClamp = 4.5f;
	public float upClamp = 4.5f;
	public float downClamp = -4.5f;
	public AudioClip deathSound;
	Animator anim;
	private GameController gameController;
	public static bool canShoot = true;
	bool canMove = true;
	bool isInvulnerable = false;
	public bool dying = false;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameController.isGameOver) {
			StopAllCoroutines ();
			Destroy(gameObject);
			return;
		}
		if (canMove) 
		{
			if (Input.GetKey ("a")) {
					Vector3 pos = GetComponent<Rigidbody>().position + (Vector3.left * playerSpeed * Time.deltaTime);
					pos.x = Mathf.Max (pos.x, leftClamp);			
					GetComponent<Rigidbody>().position = pos;
			}
			if (Input.GetKey ("d")) {
					Vector3 pos = GetComponent<Rigidbody>().position + (Vector3.right * playerSpeed * Time.deltaTime);
					pos.x = Mathf.Min (pos.x, rightClamp);			
					GetComponent<Rigidbody>().position = pos;
			}
			if (Input.GetKey ("w")) {
					Vector3 pos = GetComponent<Rigidbody>().position + (Vector3.up * playerSpeed * Time.deltaTime);
					pos.y = Mathf.Min (pos.y, upClamp);			
					GetComponent<Rigidbody>().position = pos;
			}
			if (Input.GetKey ("s")) {
					Vector3 pos = GetComponent<Rigidbody>().position + (Vector3.down * playerSpeed * Time.deltaTime);
					pos.y = Mathf.Max (pos.y, downClamp);			
					GetComponent<Rigidbody>().position = pos;
			}
		}
	}
	void OnTriggerEnter(Collider col){
		if (col != null) {
			if (col.tag == "Centipede" && !col.GetComponent<Centipede>().dying && !isInvulnerable && !dying) {
				canMove = false;
				canShoot = false;
				anim.SetBool ("shipdestroy", true);
				Debug.Log ("Player Hit");
				GetComponent<AudioSource>().PlayOneShot (deathSound);
				dying = true;
			}
		}
	}

	void moveToStart(){
		dying = false;
		anim.SetBool("shipdestroy", false);
		gameController.changeLives(-1);
		transform.position = new Vector3(0, 0, 0);
		canMove = true;
		canShoot = true;
		StartCoroutine (invulnerAbility ());
	}

	IEnumerator invulnerAbility()
	{
		isInvulnerable = true;
		for (int timer = 0; timer < 5; timer++) {
			//change to red
			Color c = gameObject.GetComponent<Renderer>().material.color;
			c.r = (0.5f);
			c.b = (0.5f);
			gameObject.GetComponent<Renderer>().material.color = c;
			yield return new WaitForSeconds (0.25f);
			//change to normal
			c.r = (1);
			c.b = (1);
			gameObject.GetComponent<Renderer>().material.color = c;
			yield return new WaitForSeconds (0.25f);
		}
		isInvulnerable = false;
	}
}