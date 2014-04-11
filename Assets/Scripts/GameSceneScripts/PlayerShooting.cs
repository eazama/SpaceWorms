using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

	public Rigidbody bulletVPrefab;
	public Rigidbody bulletHPrefab;
	public int bulletSpeed = 500;
	public float offsetValue = .7f;
	public AudioClip laser_shoot;


	public bool uBullet = false, dBullet = false, lBullet = false, rBullet = false;
	// Update is called once per frame
	 void Update () {
		if(Input.GetKey (KeyCode.UpArrow) && !uBullet)
		{
			Fire('u');
			uBullet = true;
		}
		else if(Input.GetKey (KeyCode.DownArrow) && !dBullet)
		{
			Fire('d');
			dBullet = true;
		}
		else if(Input.GetKey (KeyCode.LeftArrow) && !lBullet)
		{
			Fire('l');
			lBullet = true;
		}
		else if(Input.GetKey (KeyCode.RightArrow) && !rBullet)
		{
			Fire('r');
			rBullet = true;
		}
	}

	void Fire (char direction)
	{
		audio.PlayOneShot (laser_shoot);
		if (direction == 'u') 
		{
			Rigidbody bPrefab = Instantiate (bulletVPrefab, new Vector3(transform.position.x, transform.position.y + offsetValue, transform.position.z), Quaternion.identity) as Rigidbody;
			bPrefab.rigidbody.AddForce(Vector3.up * bulletSpeed);
		} 
		else if (direction == 'd') 
		{
			Rigidbody bPrefab = Instantiate (bulletVPrefab, new Vector3(transform.position.x, transform.position.y - offsetValue, transform.position.z), Quaternion.identity) as Rigidbody;
			bPrefab.rigidbody.AddForce(Vector3.down * bulletSpeed);
		}
		else if (direction == 'l') 
		{
			Rigidbody bPrefab = Instantiate (bulletHPrefab, new Vector3(transform.position.x - offsetValue, transform.position.y, transform.position.z), Quaternion.identity) as Rigidbody;
			bPrefab.rigidbody.AddForce(Vector3.left * bulletSpeed);
		}
		else if (direction == 'r') 
		{
			Rigidbody bPrefab = Instantiate (bulletHPrefab, new Vector3(transform.position.x + offsetValue, transform.position.y, transform.position.z), Quaternion.identity) as Rigidbody;
			bPrefab.rigidbody.AddForce(Vector3.right * bulletSpeed);
		}
	}
}
