using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButton : TriggerVolume {
	[SerializeField] private float pressSpeed = 2;
	[SerializeField] private Vector3 pressOffset = Vector3.down;

	[SerializeField] private bool limitPressability = false;
	[SerializeField, ShowIf("limitPressability")] private ContinuousBehaviour pressLimitingBehaviour = null;
	[SerializeField, ShowIf("limitPressability")] private bool hasTriggeredBehaviours = true;
	[SerializeField, ShowIf("UseTriggeredBehaviours"), Indent] private TriggeredBehaviour[] triggeredBehaviours = null;
	[SerializeField] private bool hasChanneledBehaviours = false;
	[SerializeField, ShowIf("hasChanneledBehaviours"), Indent] private ContinuousBehaviour[] channeledBehaviours = null;

	private bool UseTriggeredBehaviours {
		get {
			if (!limitPressability) {
				return true;
			} else {
				return hasTriggeredBehaviours;
			}
		}
	}

	private bool inMotion = false;
	private bool isBeingHeld = false;
	private bool allowManualRelease = true;

	private void Start() {
		if (limitPressability) {
			pressLimitingBehaviour.OnBehaviourEnd += End;
		}
	}

	protected override void TriggerEnter() {
		if (!isBeingHeld) {
			isBeingHeld = true;
			StartCoroutine(nameof(ChangeState), false);

			if(limitPressability) {
				pressLimitingBehaviour.Trigger();
				allowManualRelease = false;
			}

			if (UseTriggeredBehaviours) {
				foreach (TriggeredBehaviour behaviour in triggeredBehaviours) {
					if (behaviour != pressLimitingBehaviour) {
						behaviour.Trigger();
					}
				}
			}

			if (hasChanneledBehaviours) {
				foreach (ContinuousBehaviour behaviour in channeledBehaviours) {
					if (behaviour != pressLimitingBehaviour) {
						behaviour.Trigger();
					}
				}
			}
		}
	}

	protected override void TriggerStay() {
		if (hasChanneledBehaviours) {
			foreach (ContinuousBehaviour behaviour in channeledBehaviours) {
				if (behaviour != pressLimitingBehaviour) {
					behaviour.UpdateBehaviour();
				}
			}
		}
	}

	protected override void TriggerExit() {
		if (isBeingHeld) {
			isBeingHeld = false;

			if (allowManualRelease) {
				Release();
			}
		}
	}

	private void End() {
		allowManualRelease = true;

		if (!isBeingHeld) {
			Release();
		}
	}

	private void Release() {
		if (hasChanneledBehaviours) {
			foreach (ContinuousBehaviour behaviour in channeledBehaviours) {
				if (behaviour != pressLimitingBehaviour) {
					behaviour.EndBehaviour();
				}
			}
		}
		StartCoroutine(nameof(ChangeState), true);
	}

	private IEnumerator ChangeState(bool releasing) {
		yield return new WaitUntil(() => !inMotion);

		Vector3 start = transform.position;
		Vector3 end = transform.position + (releasing ? -pressOffset : pressOffset);

		inMotion = true;
		for (float t = 0; t < 1; t += Time.deltaTime * pressSpeed) {
			transform.position = Vector3.Lerp(start, end, t);
			yield return new WaitForEndOfFrame();
		}
		inMotion = false;

		transform.position = end;
	}

	private void OnDestroy() {
		if (limitPressability) {
			pressLimitingBehaviour.OnBehaviourEnd -= End;
		}
	}
}
