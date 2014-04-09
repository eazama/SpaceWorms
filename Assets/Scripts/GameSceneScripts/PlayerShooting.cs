using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

	public Rigidbody bulletVPrefab;
	public Rigidbody bulletHPrefab;
	public float attackSpeed = .5f;
	public int bulletSpeed = 500;
	float coolDown;
	public int bulletsout;
	public int maxBullets = 2;

	public float offsetValue = .7f;
	// Update is called once per frame
	 void Update () {
		if (Time.time >= coolDown || bulletsout < maxBullets) 
		{
			if(Input.GetKey (KeyCode.UpArrow))
			{
				Fire('u');
			}
			else if(Input.GetKey (KeyCode.DownArrow))
			{
				Fire('d');
			}
			else if(Input.GetKey (KeyCode.LeftArrow))
			{
				Fire('l');
			}
			else if(Input.GetKey (KeyCode.RightArrow))
			{
				Fire('r');
			}
		}
	}

	void Fire (char direction)
	{
		bulletsout++;
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
		coolDown = Time.time + attackSpeed;
	}

	public void BulletDestroyed(){ bulletsout -= 1;}
}
