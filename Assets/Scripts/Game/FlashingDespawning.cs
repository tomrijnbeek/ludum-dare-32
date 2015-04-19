using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class FlashingDespawning : MonoBehaviourBase {
	
	public float duration;
	public float timeLeft { get; private set; }
	public float flashingFrequency;
	public float nextSwitch { get; private set; }
	
	public bool visible;
	
	// Use this for initialization
	void Start () {
		this.visible = false;
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
				Destroy (this.gameObject);
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
		sprites.color = sprites.color.WithAlpha(Mathf.Max(0, timeLeft + nextSwitch) / duration);
	}
}
