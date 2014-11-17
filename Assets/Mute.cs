using UnityEngine;
using System.Collections;

public class Mute : MonoBehaviour {
	public GUIStyle mute;
	public GUIStyle unmute;

	public float savedVolume;
	
	// Use this for initialization
	void Start () {
		savedVolume = AudioListener.volume;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI(){
		if (AudioListener.volume != 0 && GUI.Button (new Rect(5,750,50,50), "", mute)) {
			savedVolume = AudioListener.volume;
			AudioListener.volume = 0;
		}
		else if (AudioListener.volume == 0 && GUI.Button (new Rect(5,750,50,50), "", unmute)) {
			AudioListener.volume = savedVolume;
		}
	}
	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
	
}
