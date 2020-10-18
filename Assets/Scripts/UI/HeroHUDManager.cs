using UnityEngine;

using DungeonRaid.Characters.Heroes;
using UnityEngine.InputSystem;

namespace DungeonRaid.UI {
	public class HeroHUDManager : MonoBehaviour {
		[SerializeField] private HeroHUD[] heroHUDs = null;

		private int heroCount = 0;

		public void OnPlayerJoined(PlayerInput obj) {
			AddHero(obj.GetComponent<Hero>());
		}

		private void AddHero(Hero hero) {
			heroHUDs[heroCount].SetHero(hero);

			heroCount++;
		}
	}
}