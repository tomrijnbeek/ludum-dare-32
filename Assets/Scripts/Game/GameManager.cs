using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {

	public float score;

	Planet planet;

	// Use this for initialization
	void Start () {
		this.planet = Planet.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		score += Time.deltaTime;
	}

	public void OnAlienDestroyed(Alien a)
	{
		this.score += a.score;
	}
}
