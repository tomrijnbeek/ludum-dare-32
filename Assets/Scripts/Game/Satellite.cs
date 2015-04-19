using UnityEngine;
using System.Collections;

public class Satellite : MonoBehaviourBase, IDraggable {

	private bool isFree, dragging;
	private ProjectedPath path;

	public GameObject pathPrefab;
	public Sprite[] sprites;

	public float mass;

	// Use this for initialization
	void Start () {
		this.GetComponent<SpriteRenderer>().sprite = this.sprites[Random.Range(0, this.sprites.Length)];
	}
	
	// Update is called once per frame
	void Update () {
		if (dragging)
		{
			path.velocity = TangentialVelocity() + MouseVelocity();
		}
	}

	void LeaveOrbit()
	{
		var orbit = this.GetComponent<Orbit>();
		var mass = gameObject.AddComponent<Mass>();

		mass.velocity = this.TangentialVelocity();
		mass.mass = this.mass;
		
		Component.Destroy(orbit);
        this.isFree = true;
    }
    
    void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Enemy")
		{
			coll.gameObject.SendMessage("OnSatelliteHit");
			
			//var explosion = Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
			//GameObject.Destroy(explosion, .2f);

			SatelliteManager.Instance.OnSatelliteDestroyed(this);
			GameObject.Destroy(this.gameObject);
		}
	}

	public bool MouseDown()
	{
		if (isFree || dragging) return false;

		this.dragging = true;

		var pathObj = Instantiate(pathPrefab);
		pathObj.transform.parent = transform;
		this.path = pathObj.GetComponent<ProjectedPath>();
		this.path.mass = this.mass;

		return true;
	}

	public void MouseUp()
	{
		if (!dragging) return;

		LeaveOrbit();
		GetComponent<Mass>().velocity += MouseVelocity();

		Destroy (this.path.gameObject);
		this.path = null;
		dragging = false;
	}

	Vector3 TangentialVelocity()
	{
		var orbit = this.GetComponent<Orbit>();

		var diff = Planet.Instance.transform.position - this.transform.position;
		var r = diff.magnitude;
		var dir = diff / r;
		return Mathf.Deg2Rad * orbit.angularV * r * new Vector3(dir.y, -dir.x, 0);
	}

	Vector3 MouseVelocity()
	{
		var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var rawForce = worldPos - transform.position;
		
		const float transferFactor = .7f;
		const float maxVelocity = 3.5f;

		return Vector3.ClampMagnitude(transferFactor * rawForce, maxVelocity);
	}
}
