using DungeonRaid.Characters.Bosses;
using DungeonRaid.Characters.Heroes;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameHandler : Persistent {
	[SerializeField] private StaticGameObjectArray heroPrefabs = null;

	public void OnGameStart() {
		SceneManager.LoadScene("CharacterSelect");
	}

	public void OnCharacterSelectDone(int[] heroIndices) {
		StartCoroutine(nameof(StartGameAsync), new GameStartInfo() { bossName = "Pinhead", heroIndices = heroIndices });
	}


	private IEnumerator StartGameAsync(GameStartInfo info) {
		yield return SceneManager.LoadSceneAsync(info.bossName);

		FindObjectOfType<Boss>().UpdateHealth(info.heroIndices.Length);

		GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Spawnpoint");
		for (int i = 0; i < info.heroIndices.Length; i++) {
			GameObject heroObj = Instantiate(heroPrefabs[info.heroIndices[i]], spawnPoints[i].transform.position, Quaternion.Euler(0, 180, 0));
			heroObj.GetComponent<Hero>().UpdateHealth(info.heroIndices.Length);
		}
	}

	private struct GameStartInfo {
		public string bossName;
		public int[] heroIndices;
	}
}
