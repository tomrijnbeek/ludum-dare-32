using UnityEngine;
using System.Collections;

public class Planet : Singleton<Planet> {

	public float health, maxHealth;

	// Use this for initialization
	void Start () {
		this.health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ApplyDamage (float amount) {
		this.health -= amount;
	}
}
