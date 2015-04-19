using UnityEngine;
using System.Collections;

public class DescendToOrbit : MonoBehaviour {

	public float height, speed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		var diff = Planet.Instance.transform.position - transform.position;
		if (diff.sqrMagnitude <= height * height)
		{
			gameObject.SendMessage("OrbitReached");
			Destroy (this);
			return;
		}

		transform.position = transform.position + speed * Time.deltaTime * diff.normalized * Mathf.Min (1, diff.sqrMagnitude);
	}
}
