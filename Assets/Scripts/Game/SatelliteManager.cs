using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SatelliteManager : Singleton<SatelliteManager> {

	public GameObject satellitePrefab;

	public int initialSatellites, maxSatellites, fasterSpawnThreshold;
	public float minRadius, maxRadius;
	public float timeBetweenSpawns;
	public float nextSpawn { get; private set; }

	public List<Satellite> satellites;

	public float f;

	// Use this for initialization
	void Start () {
		this.satellites = new List<Satellite>();

		for (int i = 0; i < initialSatellites; i++)
			SpawnSatellite();

		nextSpawn = timeBetweenSpawns;
	}
	
	// Update is called once per frame
	void Update () {
		f = satellites.Count > fasterSpawnThreshold ? 1 : 1 + (1 - (float)satellites.Count / (fasterSpawnThreshold + 1)) * .5f * timeBetweenSpawns;

		nextSpawn -= Time.deltaTime * f;
		if (nextSpawn < 0 && satellites.Count < maxSatellites)
		{
			SpawnSatellite();
			nextSpawn = timeBetweenSpawns;
		}
	}

	public void OnSatelliteDestroyed(Satellite satellite)
	{
		satellites.Remove(satellite);
	}

	void SpawnSatellite()
	{
		var obj = Instantiate(satellitePrefab);

		var r = Random.Range(minRadius, maxRadius);
		var v = Mathf.Sqrt(612.5f / r);

		obj.transform.position = r * Vector3.up;
		obj.transform.RotateAround(Vector3.zero, Vector3.forward, Random.Range(0, 360));

		var orbit = obj.GetComponent<Orbit>();
		orbit.angularV = Random.value < .5 ? -v : v;

		this.satellites.Add(obj.GetComponent<Satellite>());
	}
}
