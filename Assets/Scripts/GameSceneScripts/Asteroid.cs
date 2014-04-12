using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public Asteroid debris = null;
	public int health = 4;
	// Use this for initialization
	void Start () {
		gameObject.transform.eulerAngles = new Vector3(0,0,Random.Range (0,360));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Bullet") {
			col.gameObject.SetActive(false);
			health--;
			if(health <=0){
				Destroy (gameObject);
			}
		}
	}
}
