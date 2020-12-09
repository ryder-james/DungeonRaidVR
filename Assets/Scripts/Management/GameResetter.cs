using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonRaid {
	public class GameResetter : MonoBehaviour {
		private void Start() {
			GameHandler game = FindObjectOfType<GameHandler>();
			if (game == null) {
				SceneManager.LoadScene("MainMenu");
			}
		}
	}
}