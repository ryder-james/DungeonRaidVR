using UnityEngine;
using UnityEngine.UI;

using DungeonRaid.Collections;

namespace DungeonRaid.UI {
	public class MeterUI : MonoBehaviour {
		[SerializeField] private Slider slider = null;

		public MeterComponent Meter { get; set; }

		private void Awake() {
			Meter.OnValueChanged += UpdateSlider;
		}

		private void Start() {
			slider.fillRect.GetComponent<Image>().color = Meter.Color;
		}

		public void UpdateSlider() {
			slider.value = Meter.Value / Meter.MaxValue;
		}
	}
}