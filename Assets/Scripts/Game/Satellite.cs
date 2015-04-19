using UnityEngine;
using System.Collections;

public class Satellite : MonoBehaviourBase, IDraggable {

	private bool isFree, dragging;
	private ProjectedPath path;

	public GameObject pathPrefab, rocketPrefab;
	public Sprite[] sprites;

	public float mass;

	public float maxFreeTime;

	GameObject skyscraper;

	public float leaveDelay, timeUntilLeave;
	public Vector3 mouseVelocity;

	// Use this for initialization
	void Start () {
		this.GetComponent<SpriteRenderer>().sprite = this.sprites[Random.Range(0, this.sprites.Length)];
	}
	
	// Update is called once per frame
	void Update () {
		if (dragging)
		{
			path.velocity = TangentialVelocity() + MouseVelocity();
			path.projectedPos = ProjectedPos();
		}

		if (skyscraper != null)
		{
			timeUntilLeave -= Time.deltaTime;
			if (timeUntilLeave <= 0)
				Shoot ();
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

		Invoke ("Despawn", maxFreeTime);
    }

	void Despawn()
	{
		var despawn = gameObject.AddComponent<FlashingDespawning>();
		despawn.duration = 2;
		despawn.flashingFrequency = 4;

		SatelliteManager.Instance.OnSatelliteDestroyed(this);
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
		if (isFree || dragging || skyscraper != null) return false;

		this.dragging = true;

		var pathObj = Instantiate(pathPrefab);
		pathObj.transform.parent = transform;
		this.path = pathObj.GetComponent<ProjectedPath>();
		this.path.mass = this.mass;

		return true;
	}

	public void MouseUp()
	{
		if (!dragging || skyscraper != null) return;

		mouseVelocity = MouseVelocity();

		var planetPosition = Planet.Instance.transform.position;
		var planetCoords = (ProjectedPos() - planetPosition).normalized * 8;

		skyscraper = Instantiate (rocketPrefab);
		skyscraper.transform.position = planetPosition + planetCoords;
		skyscraper.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(planetCoords.y, planetCoords.x) - 90, Vector3.forward);

		Destroy (this.path.gameObject);
		this.path = null;
		dragging = false;

		timeUntilLeave = leaveDelay;
	}

	void Shoot()
	{
		if (isFree)
			return;

		LeaveOrbit();
		GetComponent<Mass>().velocity += mouseVelocity;

		Destroy (skyscraper);
		skyscraper = null;
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

		var toPlanet = Planet.Instance.transform.position - transform.position;
		if (Vector3.Dot (toPlanet, rawForce) > 0)
		{
			if (Vector3.Dot (new Vector3(toPlanet.y, -toPlanet.x), rawForce) > 0)
				rawForce = new Vector3(toPlanet.y, -toPlanet.x).normalized * rawForce.magnitude;
			else
				rawForce = new Vector3(-toPlanet.y, toPlanet.x).normalized * rawForce.magnitude;
		}
		
		const float transferFactor = 1.2f;
		const float maxVelocity = 5.4f;

		return Vector3.ClampMagnitude(transferFactor * rawForce, maxVelocity);
	}

	Vector3 ProjectedPos()
	{
		var planetPosition = Planet.Instance.transform.position;

		var planetCoords = transform.position - planetPosition;
		var a = Mathf.Atan2(planetCoords.y, planetCoords.x);
		var projectedA = a + leaveDelay * GetComponent<Orbit>().angularV * Mathf.Deg2Rad;

		return planetPosition + new Vector3(Mathf.Cos (projectedA), Mathf.Sin(projectedA), 0) * planetCoords.magnitude;
	}
}
