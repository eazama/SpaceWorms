using UnityEngine;
using System.Collections;

public class TitleScript : MonoBehaviour {
	public Animator titleAnimation;

	// Use this for initialization
	void Start () {
		titleAnimation = GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void titleScreen() {
		titleAnimation.SetBool ("titleScreen", true);
	}

	public void howToPlayScreen() {
		titleAnimation.SetBool ("titleScreen", false);
	}
}
