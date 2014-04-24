using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject centipede;
	public GameObject centipedeBody;
	public GameObject asteroid;
	public Atmosphere atmos;
	public AudioSource BackgroundMusic;
	public AudioSource GameOverMusic;
	public AudioSource lifeUpSound;
	public bool playMusic;
	public float asteroidDensity; //enter a value from 0-100
	public int segmentsOut = 3;
	public int waveNumber = 1;
	public int areaNumber = 1;
	public float spawnWait = 5.0f;
	bool newWave = false, enemySpawned = true;
	public bool isGameOver = false;
	public int seed = 0;
	GameObject[] segmentCount;
	public GUIText scoreText;
	public int score;
	public GUIText lifeText;
	public static int lives;
	public GUIText gameOverText;
	public GUIText gameOverReason;
	public GUIText gameOverInstructions;
	public GameObject WhiteScreen;
	//spawns the asteroid layout, plays the music
	void Start () {
		if (seed != 0) {
			Random.seed = seed;
		} else {
			Random.seed = Random.seed;
		}
		Debug.Log("Seed: " +Random.seed);
		spawnAsteroidLayout ();
		if (playMusic) 
		{
			BackgroundMusic.Play();
		}
		gameOverInstructions.text = "";
		gameOverReason.text = "";
		gameOverText.text = "";
		score = 0;
		lives = 3;
		UpdateScore ();
		UpdateLives ();
	}
	//checks if a new wave should start
	void Update () {
		if (isGameOver) {
			StopAllCoroutines();
			if(Input.GetKeyDown (KeyCode.R))
			{
				gameOverInstructions.text = "";
				gameOverReason.text = "";
				gameOverText.text = "";
				isGameOver = false;
				Application.LoadLevel("GameScene");
			}
			if(Input.GetKeyDown (KeyCode.Escape))
			{
				Application.LoadLevel("TitleScene");
			}
			return;
		}
		segmentCount = GameObject.FindGameObjectsWithTag ("Centipede");
		segmentsOut = segmentCount.Length;
		if (segmentsOut == 0 && !newWave && enemySpawned) 
		{
			if(waveNumber % 5 == 0)
			{
				areaNumber++;
				changeArea();
				//this is probably where the area change animation would go
			}
			waveNumber++;
			newWave = true;
			enemySpawned = false;
		}

		if (newWave && !enemySpawned)
		{
			newWave = false;
			StartCoroutine(spawnEnemyWave());
		}
	}
	//function that spawns the layout
	void spawnAsteroidLayout()
	{
		for (int row = 0; row < 25; row++)
		{
			for (int col = 0; col < 25; col++)
			{
				//prevents spawning on top of planet
				if (Random.Range(0, 100) < asteroidDensity && !( row <= 15 && row > 8 && col <= 15 && col > 8))
				{
					Instantiate (asteroid, new Vector3((row * 32) + -384, (col * 32) - 384, 0), Quaternion.identity);
				}
			}
		}
	}
	//coroutine that houses the wave spawning algorithm
	IEnumerator spawnEnemyWave()
	{
		for (int i = 0; i < (waveNumber / 3); i++) 
		{
			if(waveNumber % 5 == 0)
			{
				spawnCentipede (16 + Random.Range (waveNumber/4, waveNumber/2));
				i = waveNumber;
			}
			else{
				spawnCentipede(8 + Random.Range (0, waveNumber/3));
			}
			yield return new WaitForSeconds (spawnWait); //the wait gets shorter as the waves get larger, needs lower bound.
		}
		enemySpawned = true;
	}
	//spawns a centipede with segments # of body segments at one of the 4 spawn points
	void spawnCentipede(int segments)
	{
		int loc = Random.Range (1, 5);
		Centipede centiHead;
		//Randomly pick one of four spawning locations
		if (loc == 1) { //top
			Instantiate (centipede, new Vector3 (0, 416, 0), Quaternion.identity);
			centiHead = FindObjectOfType(typeof(Centipede)) as Centipede;
		} else if (loc == 2){ //left
			Instantiate (centipede, new Vector3 (-416, 0, 0), Quaternion.identity);
			centiHead = FindObjectOfType(typeof(Centipede)) as Centipede;
			centiHead.origin = "left";
			centiHead.direction = "down";
		} else if (loc == 3){ //right
			Instantiate (centipede, new Vector3 (0, -416, 0), Quaternion.identity);
			centiHead = FindObjectOfType(typeof(Centipede)) as Centipede;
			centiHead.origin = "bottom";
			centiHead.direction = "left";
		} else {//bottom
			Instantiate (centipede, new Vector3 (416, 0, 0), Quaternion.identity);
			centiHead = FindObjectOfType(typeof(Centipede)) as Centipede;
			centiHead.origin = "right";
			centiHead.direction = "up";
		}
		//body segment spawning loop
		centiHead.addSegment (segments);
	}

	public void AddScore (int newScoreValue)
	{
		int oldScoreMod = score % 12000;
		score += newScoreValue;
		int newScoreMod = score % 12000;
		UpdateScore ();
		if ( newScoreMod <= oldScoreMod) {
			lives++;
			UpdateLives ();
			lifeUpSound.Play();
		}
	}

	public void changeLives(int lifeChangeValue)
	{
		lives += lifeChangeValue;
		UpdateLives ();
	}

	void UpdateScore()
	{
		scoreText.text = "" + score;
	}

	void UpdateLives()
	{
		lifeText.text = "" + lives;
		if (lives <= 0) {
			lives = 0;
			gameOver ("You've used all of your ships");
		}
	}

	public void gameOver(string reason)
	{
		gameOverText.text = "GAME OVER";
		gameOverReason.text = reason;
		gameOverInstructions.text = "             Press 'R' to Restart \n or Esc to return to the Title Screen";
		isGameOver = true;
		BackgroundMusic.Stop ();
		if(playMusic)
		{
			GameOverMusic.Play();
		}
		if(PlayerPrefs.GetInt("Highest Score", 0) < score)
		{
			PlayerPrefs.SetInt("Highest Score", score);
		}
	}

	public void changeArea()
	{
		//this is where the telportation animation should play and the planet should change color
		atmos.health = 101;
		atmos.subtractHealth ();
	}
}
