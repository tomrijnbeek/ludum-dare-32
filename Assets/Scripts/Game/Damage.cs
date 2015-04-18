using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviourBase {

	public float damage = 1;
	
	public GameObject explosionPrefab;

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "CanReceiveDamage")
		{
			coll.gameObject.SendMessage("ApplyDamage", damage);
			
			var explosion = Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
			GameObject.Destroy(explosion, .2f);
			
			GameObject.Destroy(this.gameObject);
		}
	}
}
