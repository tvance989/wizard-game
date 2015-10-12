using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour {
	private static List<Item> itemsInRange = new List<Item> ();
	public static Item closestToPlayer;
	private static Transform player;
	
	public Text itemText;
	public string wizardClass, article;
	public Inventory inventory;

	private Transform transform;
	private Collider2D collider;
	private Renderer renderer;

	void Start () {
		transform = GetComponent<Transform> ();
		collider = GetComponent<Collider2D> ();
		renderer = GetComponent<Renderer> ();

		if (player == null)
			player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player")
			itemsInRange.Add (this);
	}
	
	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "Player")
			UpdateClosestItem ();
	}
	
	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "Player") {
			itemsInRange.Remove (this);
			UpdateClosestItem ();
		}
	}

	public void UpdateClosestItem () {
		closestToPlayer = FindClosestItem ();
		UpdateItemText ();
	}

	static Item FindClosestItem () {
		Item closest = null;
		float distance = Mathf.Infinity;
		foreach (Item item in itemsInRange) {
			Vector3 diff = item.transform.position - player.position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = item;
				distance = curDistance;
			}
		}
		return closest;
	}

	void UpdateItemText () {
		if (closestToPlayer == null)
			itemText.text = "";
		else
			itemText.text = "Press E to pick up " + closestToPlayer.wizardClass + " " + closestToPlayer.article;
	}
	
	public void Enable () {
		transform.position = player.position;

		collider.enabled = true;
		renderer.enabled = true;
	}
	
	public void Disable () {
		transform.position = new Vector3 (-100, -100, 0);

		collider.enabled = false;
		renderer.enabled = false;
	}
}
