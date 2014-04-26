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

	public void changePlanet()
	{
		transform.Rotate (Vector3.forward, Random.Range(1, 180));
		gameObject.renderer.material.color = HSVAtoRGBA.Convert(Random.Range(1, 360), 1, 1, 1);
		//changeColor here
	}
}
