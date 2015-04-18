using UnityEngine;
using System.Collections;

public class BurnInAtmosphere : MonoBehaviourBase {

	public GameObject explosionPrefab;
	bool burning;

	void Start() {}

	void OnTriggerStay2D(Collider2D coll) {
		if (!this.enabled || burning)
			return;

		if (coll.gameObject.tag == "Atmosphere")
		{
			var explosion = Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
			GameObject.Destroy(explosion, .2f);
			
			GameObject.Destroy(this.gameObject, .1f);

			this.burning = true;
		}
	}

	public void EnableIn(float seconds)
	{
		Invoke ("EnableNow", seconds);
	}

	public void EnableNow()
	{
		enabled = true;
	}
}
