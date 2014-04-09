using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public int playerSpeed = 5;
	public float leftClamp = -4.5f;
	public float rightClamp = 4.5f;
	public float upClamp = 4.5f;
	public float downClamp = -4.5f;
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey ("a"))
		{
			Vector3 pos = rigidbody.position + (Vector3.left * playerSpeed * Time.deltaTime);
			pos.x = Mathf.Max(pos.x, leftClamp);			
			rigidbody.position = pos;
		}
		if(Input.GetKey ("d"))
		{
			Vector3 pos = rigidbody.position + (Vector3.right * playerSpeed * Time.deltaTime);
			pos.x = Mathf.Min(pos.x, rightClamp);			
			rigidbody.position = pos;
		}
		if(Input.GetKey ("w"))
		{
			Vector3 pos = rigidbody.position + (Vector3.up * playerSpeed * Time.deltaTime);
			pos.y = Mathf.Min(pos.y, upClamp);			
			rigidbody.position = pos;
		}
		if(Input.GetKey ("s"))
		{
			Vector3 pos = rigidbody.position + (Vector3.down * playerSpeed * Time.deltaTime);
			pos.y = Mathf.Max(pos.y, downClamp);			
			rigidbody.position = pos;
		}
	}
}
