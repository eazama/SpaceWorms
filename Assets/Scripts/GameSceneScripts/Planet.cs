using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

	public int lives = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Centipede") {
			print("Planet hit");
			lives-=1;
		}
	}


}
