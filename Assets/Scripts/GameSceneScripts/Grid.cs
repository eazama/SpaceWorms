using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	// Use this for initialization
	void Start () {
		BoxCollider col;
		for(int i = 416; i >= 0; i-=32){
			col = gameObject.AddComponent<BoxCollider>();
			col.center = new Vector3(i,0,0);
			col.size = new Vector3(0,800,0);

			col = gameObject.AddComponent<BoxCollider>();
			col.center = new Vector3(-i,0,0);
			col.size = new Vector3(0,800,0);

			col = gameObject.AddComponent<BoxCollider>();
			col.center = new Vector3(0,i,0);
			col.size = new Vector3(800,0,0);

			col = gameObject.AddComponent<BoxCollider>();
			col.center = new Vector3(0,-i,0);
			col.size = new Vector3(800,0,0);
		}
	}
}
