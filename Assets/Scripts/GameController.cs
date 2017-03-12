using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnPositionValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	private bool gameOver;
	private bool restart;
	private int score;

	void Start()
	{
		gameOver = false;
		restart = false;
		gameOverText.text = "";
		restartText.text = "";

		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void Update()
	{
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
			}
		}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);

		while(true){
			for (int i=0;i<hazardCount;i++) {
				GameObject hazard = hazards[Random.Range(0, hazards.Length)];

				Vector3 spawnPosition = new Vector3 (Random.Range (spawnPositionValues.x, -spawnPositionValues.x),
							                        spawnPositionValues.y,
							                        spawnPositionValues.z);
				Quaternion spawnRotation = Quaternion.identity;

				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}

			yield return new WaitForSeconds (waveWait);

			if (gameOver) {
				restart = true;
				restartText.text = "Press 'R' for restart";
				break;
			}
		}
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	public void AddScore(int addScoreValue)
	{
		score += addScoreValue;
		UpdateScore ();
	}
	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
}