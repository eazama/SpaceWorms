using UnityEngine;
using System.Collections;

public class StartgameClick : MonoBehaviour {
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
		Application.LoadLevel("GameScene");
	}

	void OnMouseEnter()
	{
		titleController.cursor.currentPos = 0;
		titleController.cursor.transform.position = titleController.cursor.positions [titleController.cursor.currentPos];
	}
}
