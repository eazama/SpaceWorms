using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public int playerSpeed = 5;
	public float leftClamp = -4.5f;
	public float rightClamp = 4.5f;
	public float upClamp = 4.5f;
	public float downClamp = -4.5f;
	public AudioClip deathSound;

	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey ("a"))
		{
			Vector3 pos = rigidbody.position + (Vector3.left * playerSpeed * Time.deltaTime);
			pos.x = Mathf.Max(pos.x, leftClamp);			
			rigidbody.position = pos;
		}
		if(Input.GetKey ("d"))
		{
			Vector3 pos = rigidbody.position + (Vector3.right * playerSpeed * Time.deltaTime);
			pos.x = Mathf.Min(pos.x, rightClamp);			
			rigidbody.position = pos;
		}
		if(Input.GetKey ("w"))
		{
			Vector3 pos = rigidbody.position + (Vector3.up * playerSpeed * Time.deltaTime);
			pos.y = Mathf.Min(pos.y, upClamp);			
			rigidbody.position = pos;
		}
		if(Input.GetKey ("s"))
		{
			Vector3 pos = rigidbody.position + (Vector3.down * playerSpeed * Time.deltaTime);
			pos.y = Mathf.Max(pos.y, downClamp);			
			rigidbody.position = pos;
		}
	}
	void OnTriggerEnter(Collider col){
		if (col.tag == "Centipede" && col.gameObject.activeInHierarchy){
			transform.position = new Vector3(0, 0, 0);
			Debug.Log ("Player Hit");
			gameController.changeLives(-1);
			audio.PlayOneShot(deathSound);
		}
	}
}