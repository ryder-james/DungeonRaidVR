using UnityEngine;
using UnityEngine.SceneManagement;

namespace JCommon.Management {
	public class MusicHandler : Persistent {
		[SerializeField] private AudioSource[] sceneMusic = null;

		private AudioSource current = null;

		public void PlayMusicAt(int index) {
			if (index < 0 || index >= sceneMusic.Length) {
				Debug.LogError($"Err: Invalid index for MusicHandler:PlayMusicAt(int index), expected (0-{sceneMusic.Length - 1}), got ({index})");
				return;
			}

			if (current != null) {
				current.Stop();
			}
			current = sceneMusic[index];
			current.Play();
		}

		public void PlaySceneMusic() {
			PlayMusicAt(SceneManager.GetActiveScene().buildIndex);
		}
	}
}