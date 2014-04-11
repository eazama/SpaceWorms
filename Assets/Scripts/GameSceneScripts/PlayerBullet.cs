using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

	// Update is called once per frame
	void Update () 
	{

	}

	void OnBecameInvisible()
	{
		float xVel = transform.InverseTransformDirection(rigidbody.velocity).x;
		float yVel = transform.InverseTransformDirection(rigidbody.velocity).y;
		PlayerShooting ps = (PlayerShooting) (GameObject.Find ("Player").GetComponent ("PlayerShooting"));
		if( yVel > 0)
		{
			ps.uBullet = false;
		}
		else if( yVel < 0)
		{
			ps.dBullet = false;
		}
		else if( xVel < 0)
		{
			ps.lBullet = false;
		}
		else if( xVel > 0)
		{
			ps.rBullet = false;
		}
		Destroy (gameObject);
	}

}
