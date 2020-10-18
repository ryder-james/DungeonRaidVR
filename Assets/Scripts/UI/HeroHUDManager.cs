using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DungeonRaid.Characters.Heroes;
using DungeonRaid.Collections;

public class HeroHUDManager : MonoBehaviour {
	[SerializeField] private HeroHUD[] heroHUDs = null;

	private int heroCount = 0;

	public void AddHero(Hero hero) {
		heroHUDs[heroCount].SetHero(hero);

		heroCount++;
	}
}
