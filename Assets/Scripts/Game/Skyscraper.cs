using UnityEngine;
using System.Collections;

public class Skyscraper : MonoBehaviourBase {

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag != "Satellite")
			return;

		coll.gameObject.SendMessage("LeaveOrbit");
		coll.gameObject.GetComponent<Mass>().velocity += .8f * this.GetComponent<Mass>().velocity;
		Destroy(this);
	}
}
