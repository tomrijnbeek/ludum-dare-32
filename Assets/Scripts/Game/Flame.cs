using UnityEngine;
using System.Collections;

public class Flame : MonoBehaviourBase {

	public Color[] colours;
	private SpriteRenderer sprite;

	public float nextChange;
	public float minTimeBetweenColours, maxTimeBetweenColours;

	// Use this for initialization
	void Start () {
		sprite = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		this.nextChange -= Time.deltaTime;

		if (this.nextChange <= 0)
		{
			sprite.color = this.colours[Random.Range (0, this.colours.Length)];

			this.nextChange = Random.Range(minTimeBetweenColours, maxTimeBetweenColours);
		}
	}
}
