using UnityEngine;
using System.Collections;

public class Mass : MonoBehaviour {

	Planet planet;

	public Vector3 velocity;

	// Use this for initialization
	void Start () {
		planet = Planet.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		var diff = planet.transform.position - this.transform.position;
		var r = diff.magnitude;
		this.velocity += diff / (r * r * r) * planet.mass * Time.deltaTime;
		this.transform.position = this.transform.position + Time.deltaTime * velocity;
	}
}
