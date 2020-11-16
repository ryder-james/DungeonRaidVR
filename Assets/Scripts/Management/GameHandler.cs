using DungeonRaid.Characters.Bosses;
using DungeonRaid.Characters.Heroes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameHandler : Persistent {
	[SerializeField] private StaticGameObjectArray heroPrefabs = null;

	private Boss boss;
	private Hero[] heroes;
	private int deadHeroes = 0;

	public void OnGameStart() {
		SceneManager.LoadScene("CharacterSelect");
	}

	public void OnCharacterSelectDone(int[] heroIndices) {
		StartCoroutine(nameof(StartGameAsync), new GameStartInfo() { bossName = "Pinhead", heroIndices = heroIndices });
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
	}

	private void OnHeroDeath() {
		deadHeroes++;
		if (deadHeroes >= heroes.Length) {
			Debug.Log("boss wins");
		}
	}

	private void OnBossDeath() {
		Debug.Log("heroes win");
	}

	private struct GameStartInfo {
		public string bossName;
		public int[] heroIndices;
	}
}
