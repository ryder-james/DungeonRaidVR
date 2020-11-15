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

		PlayerInputManager manager = FindObjectOfType<PlayerInputManager>();
		GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Spawnpoint");
		Debug.Log(info.heroIndices.Length);
		for (int i = 0; i < info.heroIndices.Length; i++) {
			Debug.Log("calling join");
			GameObject heroObj = Instantiate(heroPrefabs[info.heroIndices[i]], spawnPoints[i].transform.position, Quaternion.Euler(0, 180, 0));
			heroObj.GetComponent<Hero>().UpdateHealth(info.heroIndices.Length);
		}
	}

	private struct GameStartInfo {
		public string bossName;
		public int[] heroIndices;
	}
}
