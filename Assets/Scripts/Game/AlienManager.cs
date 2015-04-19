using UnityEngine;
using System.Collections;

public class AlienManager : MonoBehaviourBase {

	public GameObject alienPrefab;

	public float aliensPerSecond;
	public float aliensPerSecondGrowth;

	public float nextAlien { get; private set; }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		aliensPerSecond += aliensPerSecondGrowth * Time.deltaTime;
		nextAlien -= Time.deltaTime;

		if (nextAlien <= 0)
		{
			SpawnAlien();
			nextAlien = 1 / aliensPerSecond;
		}
	}

	void SpawnAlien()
	{
		var obj = Instantiate(alienPrefab);
		
		var r = 30;
		
		obj.transform.position = r * Vector3.up;
		obj.transform.RotateAround(Vector3.zero, Vector3.forward, Random.Range(0, 360));

		if (Random.value < .5)
		{
			obj.GetComponent<Orbit>().angularV *= -.9f;
			obj.GetComponent<DescendToOrbit>().height += 2;
		}
	}
}
