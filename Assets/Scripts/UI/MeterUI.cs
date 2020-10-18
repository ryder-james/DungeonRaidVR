using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DungeonRaid.Collections;

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
