using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DungeonRaid.Characters.Heroes;
using DungeonRaid.Collections;

public class HeroHUD : MonoBehaviour {
	[SerializeField] private Transform meters = null;
	[SerializeField] private GameObject meterUIPrefab = null;

	public Hero Hero { get; private set; }

	public void SetHero(Hero hero) {
		Hero = hero;

		foreach (MeterComponent meter in hero.Meters) {
			MeterUI ui = Instantiate(meterUIPrefab, meters.transform).GetComponent<MeterUI>();
			ui.Meter = meter;
		}
	}
}
