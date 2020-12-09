using UnityEngine;
using UnityEngine.SceneManagement;

namespace JCommon.Management {
	public class MusicHandler : Persistent {
		[SerializeField] private AudioSource[] sceneMusic = null;

		private AudioSource current = null;

		public void PlayMusicAt(int index) {
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