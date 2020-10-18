using UnityEngine;

using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.UI {
	public class HeroHUDManager : MonoBehaviour {
		[SerializeField] private HeroHUD[] heroHUDs = null;

		private int heroCount = 0;

		public void AddHero(Hero hero) {
			heroHUDs[heroCount].SetHero(hero);

			heroCount++;
		}
	}
}