using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

	public int lives = 3;

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Centipede") {
			print("Planet hit");
			lives-=1;
		}
	}


}
