using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

	public Rigidbody bulletVPrefab;
	public Rigidbody bulletHPrefab;
	public int bulletSpeed = 500;
	public float offsetValue = .7f;
	public AudioClip laser_shoot;

	public Rigidbody upBullet;
	public Rigidbody downBullet;
	public Rigidbody leftBullet;
	public Rigidbody rightBullet;

	void Start(){
		upBullet = Instantiate (bulletVPrefab) as Rigidbody;
		downBullet = Instantiate (bulletVPrefab) as Rigidbody;
		leftBullet = Instantiate (bulletHPrefab) as Rigidbody;
		rightBullet = Instantiate (bulletHPrefab) as Rigidbody;
		upBullet.gameObject.SetActive (false);
		downBullet.gameObject.SetActive (false);
		leftBullet.gameObject.SetActive (false);
		rightBullet.gameObject.SetActive (false);
	}

	// Update is called once per frame
	 void Update () {
		if(Input.GetKey (KeyCode.UpArrow) && !upBullet.gameObject.activeInHierarchy)
		{
			upBullet.gameObject.SetActive(true);
			upBullet.transform.position = new Vector3(transform.position.x,
			                                          transform.position.y + offsetValue,
			                                          transform.position.z);
			upBullet.AddForce(Vector3.up * bulletSpeed);
		}
		else if(Input.GetKey (KeyCode.DownArrow) && !downBullet.gameObject.activeInHierarchy)
		{
			downBullet.gameObject.SetActive(true);
			downBullet.transform.position = new Vector3(transform.position.x,
			                                            transform.position.y - offsetValue,
			                                            transform.position.z);
			downBullet.AddForce(Vector3.down * bulletSpeed);
		}
		else if(Input.GetKey (KeyCode.LeftArrow) && !leftBullet.gameObject.activeInHierarchy)
		{
			leftBullet.gameObject.SetActive(true);
			leftBullet.transform.position = new Vector3(transform.position.x - offsetValue,
			                                            transform.position.y,
			                                            transform.position.z);
			leftBullet.AddForce(Vector3.left * bulletSpeed);
		}
		else if(Input.GetKey (KeyCode.RightArrow) && !rightBullet.gameObject.activeInHierarchy)
		{
			rightBullet.gameObject.SetActive(true);
			rightBullet.transform.position = new Vector3(transform.position.x + offsetValue,
			                                             transform.position.y,
			                                             transform.position.z);
			rightBullet.AddForce(Vector3.right * bulletSpeed);
		}
	}
}
