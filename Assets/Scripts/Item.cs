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
	private bool isInRange;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		transform = GetComponent<Transform> ();
		collider = GetComponent<Collider2D> ();
		renderer = GetComponent<Renderer> ();

		itemText.text = "";
		isInRange = false;
	}

	void Update () {
		if (isInRange && !Input.GetKey (KeyCode.LeftShift)) {
			if (Input.GetKeyDown (KeyCode.Z))
				inventory.PickUp (this, 0);
			else if (Input.GetKeyDown (KeyCode.X))
				inventory.PickUp (this, 1);
			else if (Input.GetKeyDown (KeyCode.C))
				inventory.PickUp (this, 2);
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			itemText.text = "Pick up " + wizardClass + " " + article;
			isInRange = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		itemText.text = "";
		isInRange = false;
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
