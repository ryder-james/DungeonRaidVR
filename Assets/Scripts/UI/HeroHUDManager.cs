using System.Collections;

using UnityEngine;

using DungeonRaid.Characters.Heroes;
using UnityEngine.InputSystem;

namespace DungeonRaid.UI {
	public class HeroHUDManager : MonoBehaviour {
		[SerializeField] private HeroHUD[] heroHUDs = null;

		private int heroCount = 0;

		private void Start() {
			Cursor.visible = false;
		}

		public void OnPlayerJoined(PlayerInput obj) {
			StartCoroutine(nameof(AddHero), obj.GetComponent<Hero>());
		}

		private IEnumerator AddHero(Hero hero) {
			yield return new WaitUntil(() => hero.Initialized);

			heroHUDs[heroCount].SetHero(hero);

			heroCount++;
		}
	}
}