using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DungeonRaid.Abilities;

namespace DeungonRaid.UI {
	public class AbilityHUD : MonoBehaviour{
		[SerializeField] private Slider cooldownSlider = null;
		[SerializeField] private TMP_Text cooldownText = null;
		[SerializeField] private Image tooExpensiveWarning = null;

		public Ability Ability { get; set; }

		public void StartCooldown() {
			StartCoroutine(nameof(RunCooldownAsync), Ability.Cooldown);
		}

		private void Update() {
			tooExpensiveWarning.enabled = !Ability.CanCast();
		}

		private IEnumerator RunCooldownAsync(float time) {
			cooldownSlider.value = 1;
			cooldownText.enabled = true;
			for (float t = time; t >= 0; t -= Time.deltaTime) {
				cooldownText.text = Mathf.CeilToInt(t).ToString();
				cooldownSlider.value = t / time;
				yield return null;
			}
			cooldownText.enabled = false;
			cooldownSlider.value = 0;
		}
	}
}
