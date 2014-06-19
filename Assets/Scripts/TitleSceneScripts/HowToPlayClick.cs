using UnityEngine;
using System.Collections;

public class HowToPlayClick : MonoBehaviour {

	private TitleController titleController;
	
	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("TitleController");
		if (gameControllerObject != null) {
			titleController = gameControllerObject.GetComponent <TitleController>();
		}
	}
	
	void OnMouseDown()
	{
		titleController.loadInstructions ();
	}

	void OnMouseEnter()
	{
		if(titleController.cursor.currentPos != 1){
			titleController.cursor.currentPos = 1;
			titleController.cursor.positionChanged ();
			titleController.cursor.transform.position = titleController.cursor.positions [titleController.cursor.currentPos];
		}
	}
}
