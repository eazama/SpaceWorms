using UnityEngine;
using System.Collections;

public class Active : MonoBehaviour {

	public bool isActive = true;

	// Use this for initialization
	void Start () {
		gameObject.SetActive (isActive);
	}
}
