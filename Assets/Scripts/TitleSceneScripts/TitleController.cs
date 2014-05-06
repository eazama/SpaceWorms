using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour {

	public PlayerCursor cursor;
	public GameObject keySprite;
	public GUIText WASDArrowsText;
	public GUIText spacebar;
	public GUIText instructions;
	public GUIText HighestScoreText;
	public GUIText ScoreNumText;
	public TextScript title;
	public TextScript startGameText;
	public TextScript howToPlayText;
	public TextScript highScoreText;
	
	//Animator titleAni;
	bool inMenu;
	// Use this for initialization
	void Start () {
		inMenu = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.Return)) {
			if(inMenu && cursor.currentPos == 3)
			{
				WASDArrowsText.text = "";
				spacebar.text = "";
				instructions.text = "";
				HighestScoreText.text = "";
				ScoreNumText.text = "";
				keySprite.transform.position = new Vector3(-16, 0, 0);
				cursor.currentPos = 0;
				cursor.transform.position = cursor.positions[0];
				inMenu = false;
				title.titleScreen();
				startGameText.titleScreen();
				howToPlayText.titleScreen();
				highScoreText.titleScreen();
			}
			else if(cursor.currentPos == 0 && !inMenu)
			{
				Application.LoadLevel("GameScene");
			}
			else if(cursor.currentPos == 1 && !inMenu)
			{
				loadInstructions();
			}
			else if (cursor.currentPos == 2 && !inMenu)
			{
				loadHighScores();
			}
		}
	}

	public void loadHighScores()
	{
		cursor.currentPos = 3;
		cursor.transform.position = cursor.positions[3];
		HighestScoreText.text = "High Scores:";
		for (int i = 1; i <=10; i++)
		{
			if (i == 10)
			{
				ScoreNumText.text += i +"         " + PlayerPrefs.GetInt("High Score" + i, 0) +"\n";
			}
			else 
			{
				ScoreNumText.text += i +"          " + PlayerPrefs.GetInt("High Score" + i, 0) +"\n";
			}
		}
		inMenu = true;
		startGameText.notTitleScreen();
		howToPlayText.notTitleScreen();
		highScoreText.notTitleScreen();
	}

	public void loadInstructions()
	{
		cursor.currentPos = 3;
		cursor.transform.position = cursor.positions[3];
		WASDArrowsText.text = "Move                                   Shoot in specific direction";
		spacebar.text = "Shoot All Available Shots";
		instructions.text = "Destroy the oncoming waves of Space Worms before they\n" +
			"devour the planets' atmospheres to get score!\n" +
				"You gain a life every 12000 points";
		keySprite.transform.position = new Vector3(0, -1, 0);
		inMenu = true;
		title.notTitleScreen();
		startGameText.notTitleScreen();
		howToPlayText.notTitleScreen();
		highScoreText.notTitleScreen();
	}

	void OnMouseDown()
	{
		if(inMenu && cursor.currentPos == 3)
		{
			WASDArrowsText.text = "";
			spacebar.text = "";
			instructions.text = "";
			HighestScoreText.text = "";
			ScoreNumText.text = "";
			keySprite.transform.position = new Vector3(-16, 0, 0);
			cursor.currentPos = 0;
			cursor.transform.position = cursor.positions[0];
			inMenu = false;
			title.titleScreen();
			startGameText.titleScreen();
			howToPlayText.titleScreen();
			highScoreText.titleScreen();
		}
	}
}
