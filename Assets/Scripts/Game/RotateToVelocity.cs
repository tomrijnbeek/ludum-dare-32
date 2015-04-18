using UnityEngine;
using System.Collections;

public class RotateToVelocity : MonoBehaviourBase {

	private Mass mass;

	// Use this for initialization
	void Start () {
		this.mass = this.GetComponent<Mass>();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.mass == null)
			return;

		var rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(mass.velocity.y, mass.velocity.x) - 90, Vector3.forward);
		transform.rotation = rotation;
	}
}
