using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviourBase {

	GameObject parent;
	Mass mass;

	float skyscraperMass;

	public float acceleration;

	public float velocityTransferRatio;
	public float detachSpeed;

	// Use this for initialization
	void Start () {
		parent = this.transform.parent.gameObject;
		mass = parent.GetComponent<Mass>();
		mass.velocity = this.parent.transform.TransformDirection(.02f * Vector3.left + 0.1f * Vector3.up);
		skyscraperMass = mass.mass;
		mass.mass = 1;
	}
	
	// Update is called once per frame
	void Update () {
		var forward = transform.TransformDirection(Vector3.up);
		mass.velocity += forward * acceleration * Time.deltaTime;
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
		this.mass.mass = skyscraperMass;

		var mass = this.gameObject.AddComponent<Mass>();
		mass.velocity = this.mass.velocity * velocityTransferRatio + transform.TransformDirection(Vector3.right) * detachSpeed;
		mass.mass = skyscraperMass;

		this.gameObject.AddComponent<RotateToVelocity>();
		this.GetComponent<BurnInAtmosphere>().EnableIn(.25f);
	}
}
