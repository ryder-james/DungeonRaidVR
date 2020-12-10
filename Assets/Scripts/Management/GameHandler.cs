using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

using JCommon.Management;
using JCommon.Collections;

using DungeonRaid.Characters.Bosses;
using DungeonRaid.Characters.Heroes;
using DungeonRaid.Characters.Bosses.Interactables;

namespace DungeonRaid {
	public class GameHandler : Persistent {
		[SerializeField] private StaticGameObjectArray heroPrefabs = null;
		[SerializeField] private MusicHandler musicHandler = null;

		private Boss boss;
		private Hero[] heroes;
		private RoundHandler gameResetter;
		private int deadHeroes = 0;
		private bool isPaused = false;

		private Interactable[] interactables;

		private void Start() {
			AudioListener.volume = PlayerPrefs.GetFloat("Volume", 1);
			musicHandler.PlaySceneMusic();
		}

		public void OnGameStart() {
			SceneManager.LoadScene("CharacterSelect");
		}

		public void OnCharacterSelectDone(int[] heroIndices) {
			StartCoroutine(nameof(StartGameAsync), new GameStartInfo() { bossName = "Pinhead", heroIndices = heroIndices });
		}

		public void OnMainMenu() {
			SceneManager.LoadScene("MainMenu");
			musicHandler.PlayMusicAt(0);
		}

		public void OnQuit() {
			Application.Quit();
		}

		public void TogglePause() {
			isPaused = !isPaused;

			SetInteractables(isPaused);
			if (isPaused) {
				Time.timeScale = 0;
				gameResetter.ShowPanel("Paused", 
					"RESUME", () => TogglePause(),
					true,
					"QUIT", () => Application.Quit());
			} else {
				Time.timeScale = 1;
				gameResetter.HidePanel();
			}
		}

		public void SetInteractables(bool interactable) {
			foreach (Interactable i in interactables) {
				i.IsInteractable = interactable;
			}
		}

		public void SetVolume(float volume) {
			PlayerPrefs.SetFloat("Volume", volume);
			AudioListener.volume = volume;
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
				heroes[i].Game = this;
			}

			gameResetter = FindObjectOfType<RoundHandler>();
			gameResetter.HidePanel();

			interactables = FindObjectsOfType<Interactable>();
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
			SetInteractables(false);

			yield return new WaitForSeconds(2);

			gameResetter.ShowPanel(message, "MAIN MENU", OnMainMenu);
		}

		private struct GameStartInfo {
			public string bossName;
			public int[] heroIndices;
		}
	}
}