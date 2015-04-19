using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {

	public bool gameOver;

	public float score;

	Planet planet;

	// Use this for initialization
	void Start () {
		this.planet = Planet.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameOver)
			return;

		score += Time.deltaTime;
	}

	public void OnAlienDestroyed(Alien a)
	{
		if (gameOver)
			return;

		this.score += a.score;
	}

	public void EndGame()
	{
		this.gameOver = true;
		UIManager.Instance.OnGameOver();

		GetComponent<InputManager>().enabled = false;
		GetComponent<SatelliteManager>().enabled = false;
		GetComponent<AlienManager>().enabled = false;
	}


	public void Restart()
	{
		Application.LoadLevel("Main");
	}
}
