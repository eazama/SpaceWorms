using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {

	public char direction;

	void OnBecameInvisible()
	{
		PlayerShooting ps = (PlayerShooting)(GameObject.Find ("Player").GetComponent ("PlayerShooting"));
		ps.bulletsout--;
		Destroy (gameObject);
	}
}
