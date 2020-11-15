﻿using System.Collections;

using UnityEngine;

using DungeonRaid.Characters.Heroes;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace DungeonRaid.UI {
	public class HeroHUDManager : MonoBehaviour {
		[SerializeField] private HeroHUD[] heroHUDs = null;

		private int heroCount = 0;

		private List<PlayerInput> playerInputs = null;

		private void Awake() {
			playerInputs = new List<PlayerInput>();
		}

		public void OnPlayerJoined(PlayerInput obj) {
			Debug.Log("join");
			if (!playerInputs.Contains(obj)) {
				playerInputs.Add(obj);
				StartCoroutine(nameof(AddHero), obj.GetComponent<Hero>());
			}
		}

		private IEnumerator AddHero(Hero hero) {
			Debug.Log("add pre-init");
			yield return new WaitUntil(() => hero.Initialized);
			Debug.Log("add post-init");

			heroHUDs[heroCount].SetHero(hero);

			heroCount++;
		}
	}
}