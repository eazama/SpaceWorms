using UnityEngine;
using System.Collections;

public class TextScript : MonoBehaviour {
	public Animator textAni;
	
	// Use this for initialization
	void Start () {
		textAni = GetComponent<Animator> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void titleScreen() {
		textAni.SetBool ("titleScreen", true);
	}
	
	public void notTitleScreen() {
		textAni.SetBool ("titleScreen", false);
	}
}