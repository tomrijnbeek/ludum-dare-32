using UnityEngine;
using System.Collections;

public class Satellite : MonoBehaviourBase {

	private bool isFree;

	public Sprite[] sprites;

	// Use this for initialization
	void Start () {
		this.GetComponent<SpriteRenderer>().sprite = this.sprites[Random.Range(0, this.sprites.Length)];
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && !isFree)
		{
			var orbit = this.GetComponent<Orbit>();
			var mass = gameObject.AddComponent<Mass>();
			var diff = Planet.Instance.transform.position - this.transform.position;
			var r = diff.magnitude;
			var dir = diff / r;
			mass.velocity = Mathf.Deg2Rad * orbit.angularV * r * new Vector3(dir.y, -dir.x, 0);

			Component.Destroy(orbit);
			this.isFree = true;
		}
	}
}
