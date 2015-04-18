using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviourBase {

	public Planet around;
	public float angularV;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.RotateAround(around.transform.position, Vector3.forward, Time.deltaTime * angularV);
	}
}
