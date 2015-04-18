using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviourBase {

	public const float minTimeBetweenShots = 2;
	public const float maxTimeBetweenShots = 3;

	public float nextShot;
	public GameObject pewPrefab;

	public AudioClip[] shootSounds;
	private new AudioSource audio;

	// Use this for initialization
	void Start () {
		nextShot = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
		audio = this.GetComponent<AudioSource>();
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

		if (this.shootSounds.Length > 0 && !AudioListener.pause)
			audio.PlayOneShot(this.shootSounds[Random.Range(0, this.shootSounds.Length)]);
	}
}
