using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {

	private Planet planet;

	// Use this for initialization
	void Start () {
		this.planet = Planet.Instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
