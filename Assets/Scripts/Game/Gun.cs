using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviourBase {

	public const float minTimeBetweenShots = 2;
	public const float maxTimeBetweenShots = 3;

	public float nextShot;
	public GameObject pewPrefab;

	// Use this for initialization
	void Start () {
		nextShot = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
	}
	
	// Update is called once per frame
	void Update () {
		nextShot -= Time.deltaTime;

		if (nextShot <= 0)
		{
			this.Shoot();
			nextShot = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
		}
	}

	void Shoot()
	{
		Instantiate(pewPrefab, this.transform.position, this.transform.rotation);
	}
}
