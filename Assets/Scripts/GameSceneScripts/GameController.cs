using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject centipede;
	public GameObject centipedeBody;
	public GameObject asteroid;
	public AudioClip BackgroundMusic;
	public bool playMusic;
	public float asteroidDensity; //enter a value from 0-100
	public int segmentsOut = 3;
	public int waveNumber = 1;
	public int areaNumber = 1;
	public float spawnWait = 5.0f;
	bool newWave = false, enemySpawned = true;
	public int seed = 0;
	GameObject[] segmentCount;
	public GUIText scoreText;
	public static int score;
	public GUIText lifeText;
	public static int lives;
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
			audio.Play ();
		}
		score = 0;
		lives = 3;
		UpdateScore ();
		UpdateLives ();
	}
	//checks if a new wave should start
	void Update () {
		segmentCount = GameObject.FindGameObjectsWithTag ("Centipede");
		segmentsOut = segmentCount.Length;
		if (segmentsOut == 0 && !newWave && enemySpawned) 
		{
			if(waveNumber % 5 == 0)
			{
				areaNumber++;
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
	//coroutine that will house the wave spawning algorithm
	IEnumerator spawnEnemyWave()
	{
		for (int i = 0; i < (waveNumber / 2); i++) 
		{
			if(waveNumber % 5 == 0)
			{
				spawnCentipede (16 + Random.Range (0, waveNumber/2));
				i = waveNumber;
			}
			else{
				spawnCentipede(8 + Random.Range (0, waveNumber/2));
			}
			yield return new WaitForSeconds (spawnWait); //the wait gets shorter as the waves get larger, needs lower bound.
		}
		enemySpawned = true;
	}
	//spawns a centipede with segments # of body segments at one of the 4 spawn points
	void spawnCentipede(int segments)
	{
		int loc = Random.Range (1, 5);
		int x = 0, y = 0;
		Centipede centiHead;
		//Randomly pick one of four spawning locations
		if (loc == 1) { //top
			Instantiate (centipede, new Vector3 (0, 416, 0), Quaternion.identity);
			centiHead = FindObjectOfType(typeof(Centipede)) as Centipede;
			y = 416;
		} else if (loc == 2){ //left
			Instantiate (centipede, new Vector3 (-416, 0, 0), Quaternion.identity);
			centiHead = FindObjectOfType(typeof(Centipede)) as Centipede;
			centiHead.origin = "left";
			centiHead.direction = "down";
			x = -416;
		} else if (loc == 3){ //right
			Instantiate (centipede, new Vector3 (0, -416, 0), Quaternion.identity);
			centiHead = FindObjectOfType(typeof(Centipede)) as Centipede;
			centiHead.origin = "bottom";
			centiHead.direction = "left";
			y = -416;
		} else {//bottom
			Instantiate (centipede, new Vector3 (416, 0, 0), Quaternion.identity);
			centiHead = FindObjectOfType(typeof(Centipede)) as Centipede;
			centiHead.origin = "right";
			centiHead.direction = "up";
			x = 416;
		}
		//body segment spawning loop
		if (segments > 0) {
			CentipedeBody centiBody;
			for (int i = 0; i < segments; i++) { //spawns a new segment, attaches the previous segment's (centiHead) next segment
				//then makes centihead the newly spawned segment so if it loops again it will repeat the same assigning
				Instantiate(centipedeBody, new Vector3(x, y, 0), Quaternion.identity);
				centiBody = FindObjectOfType(typeof(CentipedeBody)) as CentipedeBody;
				centiHead.nextSegment = centiBody;
				centiHead = centiBody;
				segmentsOut++;
			}
		}
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
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
			Application.LoadLevel (0);
		}
	}
}
