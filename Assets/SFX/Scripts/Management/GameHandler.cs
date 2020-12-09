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
		[SerializeField] private MusicHandler musicHandler = null;

		private Boss boss;
		private Hero[] heroes;
		private int deadHeroes = 0;
		private TMP_Text[] gameOverTexts;
		private GameObject[] gameOverPanels;
		private Button mainMenuButton;

		private void Start() {
			musicHandler.PlaySceneMusic();
		}

		public void OnGameStart() {
			SceneManager.LoadScene("CharacterSelect");
		}

		public void OnCharacterSelectDone(int[] heroIndices) {
			StartCoroutine(nameof(StartGameAsync), new GameStartInfo() { bossName = "Pinhead", heroIndices = heroIndices });
		}

		public void OnMainMenu() {
			mainMenuButton.onClick.RemoveListener(OnMainMenu);
			SceneManager.LoadScene("MainMenu");
			musicHandler.PlaySceneMusic();
		}

		public void OnQuit() {
			Application.Quit();
		}

		private IEnumerator StartGameAsync(GameStartInfo info) {
			yield return SceneManager.LoadSceneAsync(info.bossName);

			musicHandler.PlayMusicAt(1);

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
				StartCoroutine(nameof(EndGame), "Pinhead Wins!");
			}
		}

		private void OnBossDeath() {
			StartCoroutine(nameof(EndGame), "Heroes Win!");
		}

		private IEnumerator EndGame(string message) {
			yield return new WaitForSeconds(2);

			for (int i = 0; i < gameOverPanels.Length; i++) {
				gameOverTexts[i].text = message;
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