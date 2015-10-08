using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inventory : MonoBehaviour {
	public Item[] items;
	public Text inventoryText;
	public SpellController spellController;

	void Start () {
		UpdateText ();
	}

	void Update () {
		if (Input.GetKey (KeyCode.LeftShift)) {
			if (Input.GetKeyDown (KeyCode.Z))
				Drop (0);
			else if (Input.GetKeyDown (KeyCode.X))
				Drop (1);
			else if (Input.GetKeyDown (KeyCode.C))
				Drop (2);
		}
	}

	void UpdateText () {
		inventoryText.text = "";
		for (int i = 0; i < items.Length; i++) {
			if (items [i] == null)
				continue;
			inventoryText.text += items [i].wizardClass + " " + items [i].article + "\n";
		}
	}

	public void PickUp (Item item, int index) {
		if (items [index])
			Drop (index);

		item.Disable ();

		items [index] = item;

		spellController.UpdateAvailableSpells (items);

		UpdateText ();
	}

	void Drop (int index) {
		Item item = items [index];
		items [index] = null;

		spellController.UpdateAvailableSpells (items);

		item.Enable ();

		UpdateText ();
	}
}
