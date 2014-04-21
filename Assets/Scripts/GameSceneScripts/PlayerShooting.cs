using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

	public Rigidbody bulletPrefab;
	public int bulletSpeed = 500;
	public float offsetValue = .7f;
	public AudioClip laser_shoot;

	public Rigidbody upBullet;
	public Rigidbody downBullet;
	public Rigidbody leftBullet;
	public Rigidbody rightBullet;

	void Start(){
		upBullet = Instantiate (bulletPrefab) as Rigidbody;
		downBullet = Instantiate (bulletPrefab) as Rigidbody;
		leftBullet = Instantiate (bulletPrefab) as Rigidbody;
		rightBullet = Instantiate (bulletPrefab) as Rigidbody;
		upBullet.transform.eulerAngles = new Vector3 (0, 0, 90);
		downBullet.transform.eulerAngles = new Vector3(0, 0, 90);
		upBullet.GetComponent<PlayerBullet> ().direction = "up";
		downBullet.GetComponent<PlayerBullet> ().direction = "down";
		leftBullet.GetComponent<PlayerBullet> ().direction = "left";
		rightBullet.GetComponent<PlayerBullet> ().direction = "right";
		upBullet.gameObject.SetActive (false);
		downBullet.gameObject.SetActive (false);
		leftBullet.gameObject.SetActive (false);
		rightBullet.gameObject.SetActive (false);
	}

	// Update is called once per frame
	 void Update () {
		if(PlayerMovement.canShoot){
			if((Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.Space)) && !upBullet.gameObject.activeInHierarchy)
			{
			upBullet.gameObject.SetActive(true);
			upBullet.transform.position = new Vector3(transform.position.x,
			                                          transform.position.y + offsetValue,
			                                          transform.position.z);
			}
			else if((Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.Space)) && !downBullet.gameObject.activeInHierarchy)
			{
				downBullet.gameObject.SetActive(true);
				downBullet.transform.position = new Vector3(transform.position.x,
				                                            transform.position.y - offsetValue,
				                                            transform.position.z);
			}
			else if((Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.Space)) && !leftBullet.gameObject.activeInHierarchy)
			{
				leftBullet.gameObject.SetActive(true);
				leftBullet.transform.position = new Vector3(transform.position.x - offsetValue,
				                                            transform.position.y,
				                                            transform.position.z);
			}
			else if((Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.Space)) && !rightBullet.gameObject.activeInHierarchy)
			{
				rightBullet.gameObject.SetActive(true);
				rightBullet.transform.position = new Vector3(transform.position.x + offsetValue,
				                                             transform.position.y,
				                                             transform.position.z);
			}
		}
	}
}
