using UnityEngine;
using System.Collections;

public class ColorTest : MonoBehaviour {

	public float H;
	public float S;
	public float V;
	public float A;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<SpriteRenderer>().color = HSVAtoRGBA.Convert (H, S, V, A);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
