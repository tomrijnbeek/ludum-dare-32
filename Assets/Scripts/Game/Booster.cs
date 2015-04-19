using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviourBase {

	Transform parent;

	public float currentSpeed;
	public float acceleration;

	public float velocityTransferRatio;
	public float detachSpeed;

	// Use this for initialization
	void Start () {
		parent = this.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		currentSpeed += acceleration * Time.deltaTime;

		var forward = transform.TransformDirection(Vector3.up);
		parent.position += forward * currentSpeed * Time.deltaTime;
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Atmosphere")
			this.Detach();
	}

	void Detach()
	{
		this.transform.SetParent(null, true);

		Destroy(this);
		foreach (var flame in this.transform.GetComponentsInChildren<Flame>())
			Destroy (flame.gameObject);

		parent.gameObject.GetComponent<BurnInAtmosphere>().EnableIn(.25f);
		parent.gameObject.GetComponent<Mass>().velocity = currentSpeed * parent.TransformDirection(Vector3.up);

		var mass = GetComponent<Mass>();
		mass.enabled = true;
		mass.velocity = this.currentSpeed * transform.TransformDirection(Vector3.up) * velocityTransferRatio + transform.TransformDirection(Vector3.right) * detachSpeed;

		this.GetComponent<RotateToVelocity>().enabled = true;
		this.GetComponent<BurnInAtmosphere>().EnableIn(.25f);
	}
}
