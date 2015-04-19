using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviourBase {

	public float score = 25;
	public GameObject gunPrefab, explosionPrefab;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnSatelliteHit() {
		GameObject.Destroy(this.gameObject);
		GameManager.Instance.OnAlienDestroyed(this);

		var explosion = Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
		GameObject.Destroy(explosion, .2f);
	}

	void OrbitReached() {
		var gun = Instantiate(gunPrefab);
		gun.transform.parent = transform;
		gun.transform.localPosition = gunPrefab.transform.position;
		gun.transform.localRotation = Quaternion.identity;
	}
}
