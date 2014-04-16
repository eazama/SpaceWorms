using UnityEngine;
using System.Collections;

public class Atmosphere : MonoBehaviour {

	public int health = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void subtractHealth(){
		health--;
		Color c = gameObject.renderer.material.color;
		c.a = ((float)health / 100);
		gameObject.renderer.material.color = c;
	}
}
