using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Item : MonoBehaviour {
	public string wizardClass, article;
	public Text itemText;
	public Inventory inventory;

	private Transform transform, player;
	private Collider2D collider;
	private Renderer renderer;
	private bool isInRange, isClosest;

	void Start () {
		transform = GetComponent<Transform> ();
		collider = GetComponent<Collider2D> ();
		renderer = GetComponent<Renderer> ();

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();

		isInRange = false;
		itemText.text = "";
	}

	void Update () {
		if (isInRange && Input.GetKeyDown (KeyCode.E))
			inventory.PickUp (this);
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			isInRange = true;
			itemText.text = "Press E to pick up " + wizardClass + " " + article;
		}
	}
	
	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "Player") {
			isInRange = isClosest = false;
			itemText.text = "";
		}
	}
	
	public void Enable () {
		transform.position = player.position;

		collider.enabled = true;
		renderer.enabled = true;
	}
	
	public void Disable () {
		transform.position = new Vector3 (-100, -100, 0);
		isInRange = false;

		collider.enabled = false;
		renderer.enabled = false;
	}
}
