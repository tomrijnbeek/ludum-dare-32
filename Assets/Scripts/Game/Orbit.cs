using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviourBase {

	Planet around;
	public float angularV;
	public bool invariantRotation = true;

	// Use this for initialization
	void Start () {
		around = Planet.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		var rot = this.transform.rotation;
		this.transform.RotateAround(around.transform.position, Vector3.forward, Time.deltaTime * angularV);
		if (invariantRotation)
			this.transform.rotation = rot;
	}
}
