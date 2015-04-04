using UnityEngine;
using System.Collections;

public class Atmosphere : MonoBehaviour {

	public int health = 100;
	public GameController gameController;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (gameController.isGameOver) {
			return;
		}
		if (health <= 0) {
			gameController.gameOver("The planet's atmosphere has been depleted");
		}
	}

	public void subtractHealth(){
		health--;
		Color c = gameObject.GetComponent<Renderer>().material.color;
		c.a = ((float)health / 100);
		gameObject.GetComponent<Renderer>().material.color = c;
	}
}
