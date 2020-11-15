using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour {
	private enum LightState {
		ON,
		OFF,
		FADING_ON,
		FADING_OFF
	}
	
	[SerializeField] private float maxIntensity = 100;
	[SerializeField] private float minIntensity = 0;
	[SerializeField] private float timeOn = 3;
	[SerializeField] private float timeOff = 3;
	[SerializeField] private float switchDuration = 0.5f;
	[SerializeField] private bool lightStartsOn = true;
	[SerializeField] private Light flashingLight = null;

	private float fixedOnDuration;
	private float fixedOffDuration;
	private float fixedSwitchDuration;
	private float timeInCurrentState;

	private LightState state;

	private void Start() {
		fixedOnDuration = timeOn;
		fixedOffDuration = timeOff;
		fixedSwitchDuration = switchDuration;
		timeInCurrentState = 0;
		state = LightState.ON;
		flashingLight.intensity = lightStartsOn ? maxIntensity : minIntensity;
	}

	private void Update() {
		timeInCurrentState += Time.deltaTime;

		switch (state) {
		default:
		case LightState.ON:
			OnUpdate();
			break;
		case LightState.OFF:
			OffUpdate();
			break;
		case LightState.FADING_ON:
			FadeOnUpdate();
			break;
		case LightState.FADING_OFF:
			FadeOffUpdate();
			break;
		}
	}

	private void OnUpdate() {
		if (timeInCurrentState >= fixedOnDuration) {
			state = LightState.FADING_OFF;
			timeInCurrentState -= fixedOnDuration;
		}
	}

	private void OffUpdate() {
		if (timeInCurrentState >= fixedOffDuration) {
			state = LightState.FADING_ON;
			timeInCurrentState -= fixedOffDuration;
		}
	}

	private void FadeOnUpdate() {
		if (timeInCurrentState >= fixedSwitchDuration) {
			flashingLight.intensity = maxIntensity;
			state = LightState.ON;
			timeInCurrentState -= fixedSwitchDuration;
		} else {
			flashingLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, GetNormalizedTimelinePoint());
		}
	}

	private void FadeOffUpdate() {
		if (timeInCurrentState >= fixedSwitchDuration) {
			flashingLight.intensity = minIntensity;
			state = LightState.OFF;
			timeInCurrentState -= fixedSwitchDuration;
		} else {
			flashingLight.intensity = Mathf.Lerp(maxIntensity, minIntensity, GetNormalizedTimelinePoint());
		}
	}

	private float GetNormalizedTimelinePoint() {
		return timeInCurrentState / fixedSwitchDuration;
	}
}
