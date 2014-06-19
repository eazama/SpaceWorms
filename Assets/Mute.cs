using UnityEngine;
using System.Collections;

public class Mute : MonoBehaviour {
	public GUIStyle mute;
	public GUIStyle unmute;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI(){
		if (AudioListener.volume != 0 && GUI.Button (new Rect(5,750,50,50), "", mute)) {
			AudioListener.volume = 0;
		}
		else if (AudioListener.volume == 0 && GUI.Button (new Rect(5,750,50,50), "", unmute)) {
			AudioListener.volume = 100;
		}
	}
	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
	
}
