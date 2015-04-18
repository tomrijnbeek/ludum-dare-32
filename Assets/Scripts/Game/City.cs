using UnityEngine;
using System.Collections;

public class City : MonoBehaviourBase, IClickable {

	public GameObject rocketPrefab;

	public float timeBetweenShots = .25f;
	public float timeUntilNextShot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeUntilNextShot = Mathf.Max(0, timeUntilNextShot - Time.deltaTime);
	}

	public void OnMouseClick() {
		if (timeUntilNextShot > 0)
			return;

		Instantiate(rocketPrefab, this.transform.position, this.transform.rotation);
		timeUntilNextShot = timeBetweenShots;
	}
}
