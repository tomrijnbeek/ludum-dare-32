using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class FlashingSpawning : MonoBehaviour {

	public float duration;
	public float timeLeft { get; private set; }
	public float flashingFrequency;
	public float nextSwitch { get; private set; }

	public bool visible;

	Collider2D collider;

	// Use this for initialization
	void Start () {
		collider = GetComponent<Collider2D>();
		if (collider != null)
			collider.enabled = false;

		this.visible = true;
		nextSwitch = 1 / flashingFrequency;
		timeLeft = duration;
		ProcessVisibility();
	}
	
	// Update is called once per frame
	void Update () {
		nextSwitch -= Time.deltaTime;
		timeLeft -= Time.deltaTime;

		if (nextSwitch <= 0)
		{
			if (timeLeft <= 0)
			{
				visible = true;
				ProcessVisibility();
				if (collider != null)
					collider.enabled = true;
				Destroy (this);
				return;
			}

			visible = !visible;
			nextSwitch = 1 / flashingFrequency;
			ProcessVisibility();
		}
	}

	void ProcessVisibility()
	{
		var sprites = GetComponent<SpriteRenderer>();
		sprites.enabled = visible;
		sprites.color = sprites.color.WithAlpha(1 - Mathf.Max(0, timeLeft + nextSwitch) / duration);
	}
}
