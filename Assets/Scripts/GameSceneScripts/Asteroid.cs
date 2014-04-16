using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public Sprite size2;
	public Sprite size3;
	public Sprite size4;
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
			SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
			switch(health){
			case 3:
				sr.sprite = size2;
				break;
			case 2:
				sr.sprite = size3;
				break;
			case 1:
				sr.sprite = size4;
				break;
			}
			if(health <=0){
				Destroy (gameObject);
			}
		}
	}
}
