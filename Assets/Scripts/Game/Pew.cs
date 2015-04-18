using UnityEngine;
using System.Collections;

public class Pew : MonoBehaviourBase {

	public float speed;
	public float damage = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = this.transform.position + this.speed * Time.deltaTime * this.transform.TransformVector(Vector3.down);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "CanReceiveDamage")
		{
			coll.gameObject.SendMessage("ApplyDamage", 1);
			GameObject.Destroy(this.gameObject);
		}
	}
}
