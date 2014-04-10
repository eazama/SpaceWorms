using UnityEngine;
using System.Collections;

public class Disabled : MonoBehaviour {

	public bool Active = true;

	// Use this for initialization
	void Start () {
		gameObject.SetActive (Active);
	}
}
