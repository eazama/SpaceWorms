using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

	public string direction = "right";
	public int bulletSpeed = 500;

	// Update is called once per frame
	void Update () 
	{
		switch(direction)
		{
		case "left":
			GetComponent<Rigidbody>().position += (Vector3.left * bulletSpeed * Time.deltaTime);
			break;
		case "right":
			GetComponent<Rigidbody>().position += (Vector3.right * bulletSpeed * Time.deltaTime);
			break;
		case "up":
			 GetComponent<Rigidbody>().position += (Vector3.up * bulletSpeed * Time.deltaTime);
			break;
		case "down":
			GetComponent<Rigidbody>().position += (Vector3.down * bulletSpeed * Time.deltaTime);
			break;
		}

	}

	void OnBecameInvisible()
	{
		gameObject.SetActive(false);
	}

}
