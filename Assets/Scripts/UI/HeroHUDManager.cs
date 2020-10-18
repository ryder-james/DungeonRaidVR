using UnityEngine;

using DungeonRaid.Characters.Heroes;
using UnityEngine.InputSystem;

namespace DungeonRaid.UI {
	public class HeroHUDManager : MonoBehaviour {
		[SerializeField] private HeroHUD[] heroHUDs = null;

		private int heroCount = 0;

		public void OnPlayerJoined(PlayerInput obj) {
			Debug.Log("joined");
			AddHero(obj.GetComponent<Hero>());
		}

		private void AddHero(Hero hero) {
			Debug.Log(hero);
			heroHUDs[heroCount].SetHero(hero);

			heroCount++;
		}
	}
}