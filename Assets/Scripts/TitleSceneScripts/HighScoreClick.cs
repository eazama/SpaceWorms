using UnityEngine;
using System.Collections;

public class HighScoreClick : MonoBehaviour {
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
		titleController.loadHighScores ();
	}

	void OnMouseEnter()
	{
		titleController.cursor.currentPos = 2;
		titleController.cursor.transform.position = titleController.cursor.positions [titleController.cursor.currentPos];
	}
}
