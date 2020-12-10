using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

using TMPro;

namespace DungeonRaid {
	public class RoundHandler : MonoBehaviour {
		[SerializeField] private TMP_Text[] gameOverTexts = null;
		[SerializeField] private GameObject[] gameOverPanels = null;
		[SerializeField] private GameObject volumeSliderPanel = null;
		[SerializeField] private Slider volumeSlider = null;
		[SerializeField] private Button buttonOne = null;
		[SerializeField] private Button buttonTwo = null;
		[SerializeField] private TMP_Text buttonOneText = null;
		[SerializeField] private TMP_Text buttonTwoText = null;

		private GameHandler game;

		private void Start() {
			game = FindObjectOfType<GameHandler>();
			if (game == null) {
				SceneManager.LoadScene("MainMenu");
			}
			volumeSlider.value = AudioListener.volume * 100;
		}

		public void ShowPanel(string message, string buttonOneText, UnityAction callbackOne, bool showVolumeSlider = false, string buttonTwoText = "", UnityAction callbackTwo = null) {
			Cursor.visible = true;

			for (int i = 0; i < gameOverPanels.Length; i++) {
				gameOverTexts[i].text = message;
				gameOverPanels[i].SetActive(true);
			}

			volumeSliderPanel.SetActive(showVolumeSlider);

			buttonOne.onClick.AddListener(callbackOne);
			buttonOne.gameObject.SetActive(true);
			this.buttonOneText.SetText(buttonOneText);

			if (buttonTwoText != "") {
				buttonTwo.onClick.AddListener(callbackTwo);
				buttonTwo.gameObject.SetActive(true);
				this.buttonTwoText.SetText(buttonTwoText);
			}

			LayoutRebuilder.ForceRebuildLayoutImmediate(gameOverPanels[0].transform as RectTransform);
			LayoutRebuilder.ForceRebuildLayoutImmediate(gameOverPanels[1].transform as RectTransform);
		}

		public void HidePanel() {
			Cursor.visible = false;

			buttonOne.gameObject.SetActive(false);
			buttonTwo.gameObject.SetActive(false);

			buttonOne.onClick.RemoveAllListeners();
			buttonTwo.onClick.RemoveAllListeners();
			foreach (GameObject panel in gameOverPanels) {
				panel.SetActive(false);
			}
		}

		public void UpdateVolume() {
			game.SetVolume(volumeSlider.value / 100);
		}
	}
}