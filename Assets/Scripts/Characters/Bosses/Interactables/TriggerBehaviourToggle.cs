using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerBehaviourToggle : TriggerVolume {
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

	private bool isActive = false;
	private bool allowManualToggle = true;

	private void Start() {
		if (limitPressability) {
			pressLimitingBehaviour.OnBehaviourEnd += End;
		}
	}

	protected override void TriggerEnter() {
		if (!isActive) {
			isActive = true;
			StartCoroutine(nameof(ChangeState), false);

			if(limitPressability) {
				pressLimitingBehaviour.Trigger();
				allowManualToggle = false;
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
		if (isActive) {
			isActive = false;

			if (allowManualToggle) {
				Release();
			}
		}
	}

	private void End() {
		allowManualToggle = true;

		if (!isActive) {
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

	protected abstract IEnumerator ChangeState(bool releasing);

	private void OnDestroy() {
		if (limitPressability) {
			pressLimitingBehaviour.OnBehaviourEnd -= End;
		}
	}
}
