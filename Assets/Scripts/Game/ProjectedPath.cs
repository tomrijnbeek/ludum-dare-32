using UnityEngine;
using System.Collections;

public class ProjectedPath : MonoBehaviour {

	public int pathSegments;
	public float stepSize;
	public Vector3 velocity;
	public float mass;

	public GameObject[] children;

	void Start() {
		children = new GameObject[pathSegments];

		children[0] = new GameObject();
		children[0].transform.parent = transform;

		var sprites = children[0].AddComponent<SpriteRenderer>();
		sprites.sprite = GetComponent<SpriteRenderer>().sprite;

		for (int i = 1; i < children.Length; i++)
		{
			children[i] = Instantiate(children[0]);
			children[i].transform.parent = transform;

			sprites = children[i].GetComponent<SpriteRenderer>();
			sprites.color = sprites.color.WithAlpha((children.Length - i) * sprites.color.a / children.Length);
		}
	}

	// Update is called once per frame
	void Update () {
		var planet = Planet.Instance;

		var p = transform.parent.position;
		var v = velocity;

		for (int i = 0; i < pathSegments; i++)
		{
			var rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(v.y, v.x) - 90, Vector3.forward);

			children[i].transform.position = p;
			children[i].transform.rotation = rotation;

			// move simulation forward
			var diff = planet.transform.position - p;
			var r = diff.magnitude;

			var dt = stepSize / v.magnitude;

			v += (diff / (r * r * r)) * planet.mass * dt * mass;
			p = p + stepSize * v.normalized;
		}
	}
}
