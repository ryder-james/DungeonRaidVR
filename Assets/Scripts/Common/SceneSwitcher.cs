using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : Persistent {
	public void SwitchScene(string sceneName) {
		SceneManager.LoadScene(sceneName);
	}
}
