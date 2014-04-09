using UnityEngine;
using System.Collections;

public class Centipede : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Bullet") {
			Destroy (gameObject);
			Destroy (col.gameObject);
		}
	}
}
