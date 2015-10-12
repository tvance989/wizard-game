using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public Item hat, staff, boots;
	public Image hatIcon, staffIcon, bootsIcon;
	public SpellController spellController;

	public void PickUp (Item item) {
		Color color = item.GetComponent<SpriteRenderer> ().color;

		if (item.article == "hat") {
			Drop (hat);
			hat = item;
			hatIcon.transform.parent.GetComponent<Image> ().color = color;
		} else if (item.article == "staff") {
			Drop (staff);
			staff = item;
			staffIcon.transform.parent.GetComponent<Image> ().color = color;
		} else if (item.article == "boots") {
			Drop (boots);
			boots = item;
			bootsIcon.transform.parent.GetComponent<Image> ().color = color;
		}

		item.Disable ();

		spellController.UpdateAvailableSpells (new Item[] {hat, staff, boots});
	}

	void Drop (Item item) {
		if (item == null)
			return;

		item.Enable ();
	}
}
