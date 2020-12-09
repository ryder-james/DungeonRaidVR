using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using Sirenix.OdinInspector;

using DungeonRaid.Collections;
using DungeonRaid.Characters.Bosses;

namespace DungeonRaid.UI {
	public class MeterUI : MonoBehaviour {
		[SerializeField] private bool isBossMeter = false;
		[SerializeField, ShowIf("isBossMeter")] private Boss boss = null;
		[SerializeField] private Slider slider = null;

		public bool RightToLeft {
			get => slider.direction == Slider.Direction.RightToLeft;
			set => slider.direction = value ? Slider.Direction.RightToLeft : Slider.Direction.LeftToRight;
		}

		public MeterComponent Meter { get; set; }

		private void Start() {
			if (isBossMeter) {
				Meter = boss.FindMeter("Health");
			}
			Meter.OnValueChanged += UpdateSlider;
			slider.fillRect.GetComponent<Image>().color = Meter.Color;
		}

		public void UpdateSlider() {
			StartCoroutine(nameof(UpdateSliderAsync));
		}

		private IEnumerator UpdateSliderAsync() {
			float start = slider.value;
			float end = Meter.Value / Meter.MaxValue;
			float secondsToComplete = 0.25f;
			for (float t = 0; t < secondsToComplete; t += Time.deltaTime) {
				slider.value = Mathf.Lerp(start, end, t / secondsToComplete);
				yield return new WaitForEndOfFrame();
			}
			slider.value = end;
		}
	}
}