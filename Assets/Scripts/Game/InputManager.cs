using UnityEngine;
using System.Collections;

public class InputManager : Singleton<InputManager> {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
			CastRay();
	}

	void CastRay() {
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		var hit = Physics2D.Raycast(ray.origin, ray.direction);

		IClickable clickable;
		if (hit && (clickable = hit.collider.gameObject.GetInterfaceComponent<IClickable>()) != null)
		{
			clickable.OnMouseClick();
		}
	}
}
