using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {

	public char direction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (direction == 'u') 
		{
			Rigidbody bPrefab = Instantiate (bulletVPrefab, new Vector3(transform.position.x, transform.position.y + offsetValue, transform.position.z), Quaternion.identity) as Rigidbody;
			bPrefab.rigidbody.position +=(Vector3.up * bulletSpeed * Time.deltaTime);
		} 
		else if (direction == 'd') 
		{
			Rigidbody bPrefab = Instantiate (bulletVPrefab, new Vector3(transform.position.x, transform.position.y - offsetValue, transform.position.z), Quaternion.identity) as Rigidbody;
			bPrefab.rigidbody.position +=(Vector3.down * bulletSpeed * Time.deltaTime);
		}
		else if (direction == 'l') 
		{
			Rigidbody bPrefab = Instantiate (bulletHPrefab, new Vector3(transform.position.x - offsetValue, transform.position.y, transform.position.z), Quaternion.identity) as Rigidbody;
			bPrefab.rigidbody.position +=(Vector3.left * bulletSpeed * Time.deltaTime);
		}
		else if (direction == 'r') 
		{
			Rigidbody bPrefab = Instantiate (bulletHPrefab, new Vector3(transform.position.x + offsetValue, transform.position.y, transform.position.z), Quaternion.identity) as Rigidbody;
			bPrefab.rigidbody.position +=(Vector3.right * bulletSpeed * Time.deltaTime);
		}
		coolDown = Time.time + attackSpeed;
	}

	void OnBecameInvisible()
	{
		PlayerShooting.bulletsout -= 1;
		Destroy (gameObject);
	}
}
