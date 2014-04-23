using UnityEngine;
using System.Collections;

public class PlayerCursor : MonoBehaviour {
	public int currentPos = 0;
	public Vector3 [] positions;
	public AudioClip selectSound;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.S) && currentPos < 2) {
			currentPos++;
			transform.position = positions[currentPos];
			audio.PlayOneShot(selectSound);
		}
		if (Input.GetKeyDown (KeyCode.W) && currentPos > 0) {
			currentPos--;
			transform.position = positions[currentPos];
			audio.PlayOneShot(selectSound);
		}

		if (Input.GetKey (KeyCode.Space)) {
			if(currentPos == 0)
				Application.LoadLevel("GameScene");
		}
	}
}
