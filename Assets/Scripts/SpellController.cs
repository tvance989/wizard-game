using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public interface ISpell {
	float Cast (); // returns the spell's cooldown
}

public class SpellController : MonoBehaviour {
	public Text activeSpellText;
	public Image[] spellCooldowns;
	public GameController gameController;

	private GameObject[] spells;
	private Transform spellSpawn;
	private float[] nextCast;
	private float[] cooldownTimers;
	private int activeSpell;

	void Start () {
		spells = new GameObject[7];
		nextCast = new float[7];
		cooldownTimers = new float[7];

		spellSpawn = GetComponent<Transform> ();
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1))
			SwitchSpells (0);
		else if (Input.GetKeyDown (KeyCode.Alpha2))
			SwitchSpells (1);
		else if (Input.GetKeyDown (KeyCode.Alpha3))
			SwitchSpells (2);
		else if (Input.GetKeyDown (KeyCode.Alpha4))
			SwitchSpells (3);
		else if (Input.GetKeyDown (KeyCode.Alpha5))
			SwitchSpells (4);
		else if (Input.GetKeyDown (KeyCode.Alpha6))
			SwitchSpells (5);
		else if (Input.GetKeyDown (KeyCode.Alpha7))
			SwitchSpells (6);

		if (Input.GetButton ("Fire1")) {
			if (Time.time >= nextCast [activeSpell]) {
				Cast (activeSpell);
			}
		}

		UpdateSpellCooldowns ();
	}
	
	void Cast (int index) {
		if (spells [index] == null)
			return;

		GameObject clone = Instantiate (spells [index], spellSpawn.position, spellSpawn.rotation) as GameObject;
		cooldownTimers [index] = clone.GetComponent<ISpell> ().Cast ();
		
		spellCooldowns [index].fillAmount = 0f;
		nextCast [index] = Time.time + cooldownTimers [index];
	}

	void SwitchSpells (int index) {
		if (spells.Length <= index)
			return;

		spellCooldowns [activeSpell].transform.parent.GetComponent<Image> ().color = new Color (255, 255, 255, 0.5f);

		activeSpell = index;
		spellCooldowns [activeSpell].transform.parent.GetComponent<Image> ().color = new Color (255, 255, 255, 1f);

		activeSpellText.text = "Active Spell: " + spells [activeSpell].GetComponent<ISpell> ().GetType ().ToString ();
	}

	void UpdateSpellCooldowns () {
		//.make this into an animation so it doesn't have to update every frame
		for (int i = 0; i < spellCooldowns.Length; i++) {
			spellCooldowns [i].enabled = false;
			if (spells.Length > i)
				if (spells [i] != null)
					spellCooldowns [i].enabled = true;

			if (spellCooldowns [i].fillAmount < 1)
				spellCooldowns [i].fillAmount += Time.deltaTime / cooldownTimers [i];
		}
	}

	public void UpdateAvailableSpells (Item[] items) {
		string[] newSpells = gameController.GetSpellsFromItems (items.Where (i => i != null).ToArray ());
		
		spells = new GameObject [newSpells.Length];

		for (int i = 0; i < newSpells.Length; i++) {
			try {
				spells [i] = Resources.Load ("Spells/" + newSpells [i]) as GameObject;
			} catch {
				Debug.Log("Error: Could not load spell " + newSpells [i]);
			}
		}

		spells = spells.Where (i => i != null).ToArray ();

		for (int i = 0; i < spells.Length; i++) {
			spellCooldowns [i].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/" + spells [i].GetComponent<ISpell> ().GetType ().ToString ());
		}

		UpdateSpellCooldowns ();
	}
}
