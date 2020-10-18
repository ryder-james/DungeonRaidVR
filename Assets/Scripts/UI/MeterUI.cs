using UnityEngine;
using UnityEngine.UI;

using DungeonRaid.Collections;

namespace DungeonRaid.UI {
	public class MeterUI : MonoBehaviour {
		[SerializeField] private Slider slider = null;

		public bool RightToLeft {
			get => slider.direction == Slider.Direction.RightToLeft;
			set => slider.direction = value ? Slider.Direction.RightToLeft : Slider.Direction.LeftToRight;
		}

		public MeterComponent Meter { get; set; }

		private void Start() {
			Meter.OnValueChanged += UpdateSlider;
			slider.fillRect.GetComponent<Image>().color = Meter.Color;
		}

		public void UpdateSlider() {
			slider.value = Meter.Value / Meter.MaxValue;
		}
	}
}