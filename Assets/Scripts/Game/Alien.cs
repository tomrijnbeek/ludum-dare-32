using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviourBase {

	public float score = 25;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnSatelliteHit() {
		GameObject.Destroy(this.gameObject);
		GameManager.Instance.OnAlienDestroyed(this);
	}
}
