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
			slider.value = Meter.Value / Meter.MaxValue;
		}
	}
}