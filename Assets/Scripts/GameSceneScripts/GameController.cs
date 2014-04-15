using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject centipede;
	public GameObject asteroid;
	public AudioClip BackgroundMusic;
	public bool playMusic;
	public float asteroidDensity; //enter a value from 0-100
	public static int segmentsOut = 3;
	public int segmentsOutVisible = 0; //debugging purposes
	public int waveNumber = 1;
	public float spawnWait = 5.0f;
	bool newWave = false, enemySpawned = true;
	
	void Start () {
		spawnAsteroidLayout ();
		if (playMusic) 
		{
			audio.Play ();
		}
	}

	void Update () {
		segmentsOutVisible = segmentsOut;
		if (segmentsOut == 0 && !newWave && enemySpawned) 
		{
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

	void spawnAsteroidLayout()
	{
		for (int row = 0; row < 25; row++)
		{
			for (int col = 0; col < 25; col++)
			{
				//prevents spawning on top of planet
				if (Random.Range(0, 100) < asteroidDensity && !( row <= 14 && row > 9 && col <= 14 && col > 9))
				{
					Instantiate (asteroid, new Vector3((row * 32) + -384, (col * 32) - 384, 0), Quaternion.identity);
				}
			}
		}
	}

	IEnumerator spawnEnemyWave()
	{
		for (int i = 0; i < waveNumber; i++) 
		{
			segmentsOut++;
			Instantiate (centipede, new Vector3 (0, 416, 0), Quaternion.identity);
			yield return new WaitForSeconds (spawnWait - ( waveNumber * 0.2f)); //the wait gets shorter as the waves get larger, needs lower bound.
		}
		enemySpawned = true;
	}
}
