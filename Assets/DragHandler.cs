using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject itemBeingDragged;

	GameObject gameObject;
	Transform transform;
	Vector3 startPosition;

	void Start () {
		transform = GetComponent<Transform> ();
		gameObject = GetComponent<GameObject> ();
	}
	
	public void OnEndDrag (PointerEventData eventData) {
		itemBeingDragged = gameObject;
		startPosition = transform.position;
	}
	
	public void OnDrag (PointerEventData eventData) {
		transform.position = Input.mousePosition;
	}

	public void OnBeginDrag (PointerEventData eventData) {
		itemBeingDragged = null;
		transform.position = startPosition;
	}
}
