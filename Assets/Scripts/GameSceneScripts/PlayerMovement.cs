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
	//Collider tempCollider;
	private GameController gameController;
	public static bool canShoot = true;
	bool canMove = true;
	bool isInvulnerable = false;
	public bool dying = false;

	void Start()
	{
		//tempCollider = gameObject.collider;
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
					Vector3 pos = rigidbody.position + (Vector3.left * playerSpeed * Time.deltaTime);
					pos.x = Mathf.Max (pos.x, leftClamp);			
					rigidbody.position = pos;
			}
			if (Input.GetKey ("d")) {
					Vector3 pos = rigidbody.position + (Vector3.right * playerSpeed * Time.deltaTime);
					pos.x = Mathf.Min (pos.x, rightClamp);			
					rigidbody.position = pos;
			}
			if (Input.GetKey ("w")) {
					Vector3 pos = rigidbody.position + (Vector3.up * playerSpeed * Time.deltaTime);
					pos.y = Mathf.Min (pos.y, upClamp);			
					rigidbody.position = pos;
			}
			if (Input.GetKey ("s")) {
					Vector3 pos = rigidbody.position + (Vector3.down * playerSpeed * Time.deltaTime);
					pos.y = Mathf.Max (pos.y, downClamp);			
					rigidbody.position = pos;
			}
		}
	}
	void OnTriggerEnter(Collider col){
		if (col != null) {
			if (col.tag == "Centipede" && !col.GetComponent<Centipede>().dying && !isInvulnerable && !dying) {
				//Destroy (gameObject.collider);
				//transform.position = new Vector3(0, 0, 0);
				//gameController.changeLives(-1);
				canMove = false;
				canShoot = false;
				anim.SetBool ("shipdestroy", true);
				Debug.Log ("Player Hit");
				audio.PlayOneShot (deathSound);
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
			Color c = gameObject.renderer.material.color;
			c.r = (0.5f);
			c.b = (0.5f);
			gameObject.renderer.material.color = c;
			yield return new WaitForSeconds (0.25f);
			//change to normal
			c.r = (1);
			c.b = (1);
			gameObject.renderer.material.color = c;
			yield return new WaitForSeconds (0.25f);
		}
		isInvulnerable = false;
	}
}