using DungeonRaid.Characters.Bosses;
using DungeonRaid.Characters.Heroes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandler : Persistent {
	[SerializeField] private StaticGameObjectArray heroPrefabs = null;

	private Boss boss;
	private Hero[] heroes;
	private int deadHeroes = 0;
	private TMP_Text gameOverText;
	private GameObject gameOverPanel;
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

		gameOverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");
		gameOverText = gameOverPanel.transform.GetComponentInChildren<TMP_Text>();
		mainMenuButton = gameOverPanel.transform.GetComponentInChildren<Button>(true);
		mainMenuButton.onClick.AddListener(OnMainMenu);
		gameOverPanel.SetActive(false);
	}

	private void OnHeroDeath() {
		deadHeroes++;
		if (deadHeroes >= heroes.Length) {
			gameOverText.text = "Pinhead Wins!";
			mainMenuButton.gameObject.SetActive(true);
			gameOverPanel.SetActive(true);
		}
	}

	private void OnBossDeath() {
		gameOverText.text = "Heroes Win!";
		mainMenuButton.gameObject.SetActive(true);
		gameOverPanel.SetActive(true);
	}

	private struct GameStartInfo {
		public string bossName;
		public int[] heroIndices;
	}
}
