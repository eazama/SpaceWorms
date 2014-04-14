using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject asteroid;
	public float asteroidDensity; //enter a value from 0-100
	
	void Start () {
		spawnAsteroidLayout ();
	}

	void Update () {
	
	}

	void spawnAsteroidLayout()
	{
		for (int row = 0; row < 25; row++)
		{
			for (int col = 0; col < 25; col++)
			{
				if (Random.Range(0, 100) < asteroidDensity)
				{
					Instantiate (asteroid, new Vector3((row * 32) + -384, (col * 32) - 384, 0), Quaternion.identity);
				}
			}
		}
	}

	void spawnEnemyWave()
	{

	}
}
