using UnityEngine;
using System.Collections;

public class InputManager : Singleton<InputManager> {

	public IDraggable dragging;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && dragging == null)
			CastRay();
		else if (Input.GetMouseButtonUp(0) && dragging != null)
		{
			this.dragging.MouseUp();
			this.dragging = null;
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	void CastRay() {
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		var hit = Physics2D.Raycast(ray.origin, ray.direction);

		IDraggable draggable;
		if (hit && (draggable = hit.collider.gameObject.GetInterfaceComponent<IDraggable>()) != null && draggable.MouseDown())
		{
			dragging = draggable;
		}
	}
}
