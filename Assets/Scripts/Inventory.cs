using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public Item hat, staff, boots;
	public Image hatIcon, staffIcon, bootsIcon;
	public SpellController spellController;

	private Image hatBorder, staffBorder, bootsBorder;

	void Start () {
		hatBorder = hatIcon.transform.parent.GetComponent<Image> ();
		staffBorder = staffIcon.transform.parent.GetComponent<Image> ();
		bootsBorder = bootsIcon.transform.parent.GetComponent<Image> ();
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			PickUp (Item.closestToPlayer);
			Item.closestToPlayer.UpdateClosestItem ();
		}
	}

	public void PickUp (Item item) {
		Color color = item.GetComponent<SpriteRenderer> ().color;

		if (item.article == "hat") {
			Drop (hat);
			hat = item;
			hatBorder.color = color;
		} else if (item.article == "staff") {
			Drop (staff);
			staff = item;
			staffBorder.color = color;
		} else if (item.article == "boots") {
			Drop (boots);
			boots = item;
			bootsBorder.color = color;
		} else {
			Debug.Log ("Can't pick up " + item + ". Invalid article.");
			return;
		}

		spellController.UpdateAvailableSpells (new Item[] {hat, staff, boots});
		
		item.Disable ();
	}

	void Drop (Item item) {
		if (item == null)
			return;

		item.Enable ();
	}
}
