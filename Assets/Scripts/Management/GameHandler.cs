using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

using JCommon.Management;
using JCommon.Collections;

using DungeonRaid.Characters.Bosses;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid {
	public class GameHandler : Persistent {
		[SerializeField] private StaticGameObjectArray heroPrefabs = null;

		private Boss boss;
		private Hero[] heroes;
		private int deadHeroes = 0;
		private TMP_Text[] gameOverTexts;
		private GameObject[] gameOverPanels;
		private Button mainMenuButton;

		public void OnGameStart() {
			SceneManager.LoadScene("CharacterSelect");
		}

		public void OnCharacterSelectDone(int[] heroIndices) {
			StartCoroutine(nameof(StartGameAsync), new GameStartInfo() { bossName = "Pinhead", heroIndices = heroIndices });
		}

		public void OnMainMenu() {
			mainMenuButton.onClick.RemoveListener(OnMainMenu);
			SceneManager.LoadScene("MainMenu");
		}

		public void OnQuit() {
			Application.Quit();
		}

		private IEnumerator StartGameAsync(GameStartInfo info) {
			yield return SceneManager.LoadSceneAsync(info.bossName);

			boss = FindObjectOfType<Boss>();
			boss.UpdateHealth(info.heroIndices.Length);
			boss.OnDeath += OnBossDeath;

			heroes = new Hero[info.heroIndices.Length];

			GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Spawnpoint");
			for (int i = 0; i < info.heroIndices.Length; i++) {
				GameObject heroObj = Instantiate(heroPrefabs[info.heroIndices[i]], spawnPoints[i].transform.position, Quaternion.Euler(0, 180, 0));
				heroes[i] = heroObj.GetComponent<Hero>();
				heroes[i].UpdateHealth(info.heroIndices.Length);
				heroes[i].OnDeath += OnHeroDeath;
			}

			gameOverPanels = GameObject.FindGameObjectsWithTag("GameOverPanel");
			gameOverTexts = new TMP_Text[gameOverPanels.Length];
			for (int i = 0; i < gameOverPanels.Length; i++) {
				gameOverTexts[i] = gameOverPanels[i].transform.GetComponentInChildren<TMP_Text>();
				mainMenuButton = gameOverPanels[i].transform.GetComponentInChildren<Button>(true);
			}

			mainMenuButton.onClick.AddListener(OnMainMenu);
			foreach (GameObject panel in gameOverPanels) {
				panel.SetActive(false);
			}
		}

		private void OnHeroDeath() {
			deadHeroes++;
			if (deadHeroes >= heroes.Length) {
				for (int i = 0; i < gameOverPanels.Length; i++) {
					gameOverTexts[i].text = "Pinhead Wins!";
					gameOverPanels[i].SetActive(true);
				}

				mainMenuButton.gameObject.SetActive(true);
			}
		}

		private void OnBossDeath() {
			for (int i = 0; i < gameOverPanels.Length; i++) {
				gameOverTexts[i].text = "Heroes Win!";
				gameOverPanels[i].SetActive(true);
			}

			mainMenuButton.gameObject.SetActive(true);
		}

		private struct GameStartInfo {
			public string bossName;
			public int[] heroIndices;
		}
	}
}