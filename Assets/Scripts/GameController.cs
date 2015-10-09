using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameController : MonoBehaviour {
	private Dictionary<string,string> spells;

	void Start () {
		BuildSpellList ();
	}

	void BuildSpellList () {
		spells = new Dictionary<string,string> ();
		
		// pure spells
		spells.Add ("aer","ForcePush");
		spells.Add ("alc","Poison");
		spells.Add ("chr","ReverseTime");
		spells.Add ("cle","HealingLight");
		spells.Add ("dru","Vine");
		spells.Add ("geo","Boulder");
		spells.Add ("hyd","Wave");
		spells.Add ("nec","LeechLife");
		spells.Add ("pyr","Fireball");
		spells.Add ("pyrpyrpyr","FireCloak");
		spells.Add ("sum","SummonOni");
		
		// 2x combos
		spells.Add ("aeralc","AirborneVirus");
		spells.Add ("aerchr","Haste");
		spells.Add ("aercle","HolyGhost");
		spells.Add ("aerdru","CallEagle");
		spells.Add ("aergeo","Sandstorm");
		spells.Add ("aerhyd","Hurricane");
		spells.Add ("aernec","DeathFromAbove");
		spells.Add ("aerpyr","FlameTongue");
		spells.Add ("aersum","Ghost");
		spells.Add ("alcchr","TimeBomb");
		spells.Add ("alccle","HolyWater");
		spells.Add ("alcdru","GreenLion");
		spells.Add ("alcgeo","PhilosophersStone");
		spells.Add ("alchyd","Acid");
		spells.Add ("alcnec","Pestilence");
		spells.Add ("alcpyr","Molotov");
		spells.Add ("alcsum","");
		spells.Add ("chrcle","Regeneration");
		spells.Add ("chrdru","ColossalGrowth");
		spells.Add ("chrgeo","Slow");
		spells.Add ("chrhyd","Doubling");
		spells.Add ("chrnec","Reaper");
		spells.Add ("chrpyr","Berserk");
		spells.Add ("chrsum","Duplicate");
		spells.Add ("cledru","BirdCall");
		spells.Add ("clegeo","HealingChamber");
		spells.Add ("clehyd","WalkOnWater");
		spells.Add ("clenec","Mortification");
		spells.Add ("clepyr","CleansingFlame");
		spells.Add ("clesum","SummonAngel");
		spells.Add ("drugeo","ArmyODillos");
		spells.Add ("druhyd","Rain");
		spells.Add ("drunec","Putrefaction");
		spells.Add ("drupyr","ForestFire");
		spells.Add ("drusum","SummonBeast");
		spells.Add ("geohyd","Quicksand");
		spells.Add ("geonec","SkeletonArms");
		spells.Add ("geopyr","Magmud");
		spells.Add ("geosum","RockGolem");
		spells.Add ("hydnec","SeaOfBlood");
		spells.Add ("hydpyr","Geyser");
		spells.Add ("hydsum","SummonKraken");
		spells.Add ("necpyr","Hellfire");
		spells.Add ("necsum","SummonSkeleton");
		spells.Add ("pyrsum","SummonFireSprites");
		
		// 3x combos
		spells.Add ("aergeopyr","Soul");
	}

	public string[] GetSpellsFromItems (Item[] items) {
		items = items.Where (s => s != null).ToArray ();

		string[] classes = new string [items.Length];
		for (int i = 0; i < items.Length; i++) {
			if (items [i] == null)
				continue;
			classes [i] = items [i].wizardClass;
		}

		classes = classes.Where (s => s != null).ToArray ();
		Array.Sort (classes);

		List<string> availableSpells = new List<string>();
		string key = "";

		// 1x
		for (int i = 0; i < classes.Length; i++) {
			key = classes [i];
			try {
				availableSpells.Add (spells [key]);
			} catch {
				Debug.Log ("There is no spell defined for " + key);
			}
		}
		// 2x
		for (int i = 0; i < classes.Length; i++) {
			for (int j = 0; j < i; j++) {
				key = classes [j] + classes [i];
				try {
					availableSpells.Add (spells [key]);
				} catch {
					Debug.Log ("There is no spell defined for " + key);
				}
			}
		}
		// 3x
		if (classes.Length == 3) {
			key = string.Join ("", classes);
			try {
				availableSpells.Add (spells [key]);
			} catch {
				Debug.Log ("There is no spell defined for " + key);
			}
		}

		Debug.Log ("Available spells: " + string.Join(", ", availableSpells.ToArray()));

		return availableSpells.Distinct ().ToArray ();
	}
}
