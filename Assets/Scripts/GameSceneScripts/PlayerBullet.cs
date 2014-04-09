using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {



	// Update is called once per frame
	void Update () 
	{

	}

	void OnBecameInvisible()
	{
		PlayerShooting ps = (PlayerShooting) (GameObject.Find ("Player").GetComponent ("PlayerShooting"));
		ps.bulletsout -= 1;
		Destroy (gameObject);
	}

}
