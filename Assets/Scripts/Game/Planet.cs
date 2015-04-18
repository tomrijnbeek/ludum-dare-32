using UnityEngine;
using System.Collections;

public class Planet : Singleton<Planet> {

	public float health, maxHealth;
	public float rotationSpeed;
	public float mass;

	// Use this for initialization
	void Start () {
		this.health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed);
	}

	void ApplyDamage (float amount) {
		this.health -= amount;
	}
}
