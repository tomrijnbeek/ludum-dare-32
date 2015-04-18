using UnityEngine;
using System.Collections;

public class Planet : Singleton<Planet> {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ApplyDamage (float amount) {
		Debug.Log("damage");
	}
}
