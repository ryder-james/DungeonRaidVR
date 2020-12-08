using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using DungeonRaid.Abilities;

namespace DungeonRaid.UI {
	public class AbilityHUD : MonoBehaviour{
		[SerializeField] private Slider cooldownSlider = null;
		[SerializeField] private TMP_Text cooldownText = null;
		[SerializeField] private Image tooExpensiveWarning = null;
		[SerializeField] private Image channelingWarning = null;

		private Ability ability;
		private bool isEnabled = true;

		public Ability Ability { 
			get => ability;
			set {
				ability = value;
				ability.OnAbilityBeginCast += StartCasting;
				ability.OnAbilityEndCast += StartCooldown;
			}
		}

		public void StartCasting() {
			if (isEnabled) {
				channelingWarning.enabled = true;
			}
		}

		public void StartCooldown() {
			if (isEnabled) {
				channelingWarning.enabled = false;
				StartCoroutine(nameof(RunCooldownAsync), Ability.Cooldown);
			}
		}

		public void Disable() {
			isEnabled = false;
			StopAllCoroutines();
			cooldownSlider.value = 1;
			cooldownText.enabled = false;
			channelingWarning.enabled = false;
			tooExpensiveWarning.enabled = false;
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
				yield return new WaitForEndOfFrame();
			}

			cooldownText.enabled = false;
			cooldownSlider.value = 0;
		}
	}
}
