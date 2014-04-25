using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour {

	public PlayerCursor cursor;
	//public GUIText titleText;
	//public GUIText howToPlayText;
	//public GUIText startGameText;
	//public GUIText scoresText;
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
		if (Input.GetKeyDown (KeyCode.Space)) {
			if(inMenu && cursor.currentPos == 3)
			{
				//titleText.text = "SPACE WORMS";
				//howToPlayText.text = "How to Play";
				//startGameText.text = "Start Game";
				//scoresText.text = "High Scores";
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
				cursor.currentPos = 3;
				cursor.transform.position = cursor.positions[3];
				//titleText.text = "";
				//howToPlayText.text = "";
				//startGameText.text = "";
				//scoresText.text = "";
				WASDArrowsText.text = "Move                            Shoot in specific direction";
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
			else if (cursor.currentPos == 2 && !inMenu)
			{
				cursor.currentPos = 3;
				cursor.transform.position = cursor.positions[3];
				//titleText.text = "";
				//howToPlayText.text = "";
				//startGameText.text = "";
				//scoresText.text = "";
				HighestScoreText.text = "Highest Score:";
				ScoreNumText.text = "" + PlayerPrefs.GetInt("Highest Score", 0);
				inMenu = true;
				startGameText.notTitleScreen();
				howToPlayText.notTitleScreen();
				highScoreText.notTitleScreen();
			}
		}
	}
}
