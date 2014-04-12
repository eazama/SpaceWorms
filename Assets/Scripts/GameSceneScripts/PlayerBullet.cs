using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

	// Update is called once per frame
	void Update () 
	{

	}

	void OnBecameInvisible()
	{
		gameObject.rigidbody.velocity = new Vector3(0,0,0);
		gameObject.SetActive(false);
	}

}
