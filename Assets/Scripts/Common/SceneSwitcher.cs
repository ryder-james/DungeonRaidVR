using UnityEngine.SceneManagement;

namespace JCommon.Management {
	public class SceneSwitcher : Persistent {
		public void SwitchScene(string sceneName) {
			SceneManager.LoadScene(sceneName);
		}
	}
}
