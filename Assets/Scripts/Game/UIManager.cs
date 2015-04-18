﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : Singleton<UIManager> {

	public Text score;
	public Slider healthbar;

	private Planet planet;
	private GameManager gameManager;

	public bool muted;

	// Use this for initialization
	void Start () {
		this.planet = Planet.Instance;
		this.gameManager = GameManager.Instance;

		AudioListener.pause = muted;
	}
	
	// Update is called once per frame
	void Update () {
		score.text = string.Format("Score: {0}", Mathf.RoundToInt(gameManager.score));
		healthbar.value = Mathf.Clamp01(planet.health / planet.maxHealth);
	}

	public void ToggleSound() {
		muted = !muted;
		AudioListener.pause = muted;
	}
}