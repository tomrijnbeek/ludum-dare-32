using UnityEngine;
using System.Collections;

public class Pew : MonoBehaviourBase {

	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = this.transform.position + this.speed * Time.deltaTime * this.transform.TransformVector(Vector3.down);
	}


}
