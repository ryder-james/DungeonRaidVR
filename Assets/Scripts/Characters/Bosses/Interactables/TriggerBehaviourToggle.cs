using DungeonRaid.Abilities;
using DungeonRaid.Characters.Bosses;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerBehaviourToggle : TriggerVolume {
	protected enum ToggleState {
		Off,
		Transitioning,
		On
	}

	[System.Flags]
	protected enum LockState {
		None		= 0b0000,
		Locked		= 0b0001,
		OnCooldown	= 0b0010,
		Held		= 0b0100,
		Active		= 0b1000
	}

	[SerializeField] private bool useBehaviourAsLock = false;
	[SerializeField, ShowIf("useBehaviourAsLock")] private ContinuousBehaviour lockingBehaviour = null;
	[SerializeField, ShowIf("useBehaviourAsLock")] private bool hasTriggeredBehaviours = true;
	[SerializeField, ShowIf("UseTriggeredBehaviours"), Indent] private TriggeredBehaviour[] triggeredBehaviours = null;
	[SerializeField] private bool hasChanneledBehaviours = false;
	[SerializeField, ShowIf("hasChanneledBehaviours"), Indent] private ContinuousBehaviour[] channeledBehaviours = null;
	[Space]
	[SerializeField] private bool hasCooldown = false;
	[SerializeField, ShowIf("hasCooldown")] private float cooldown = 2;
	[Space]
	[SerializeField] private Cost triggerCost = new Cost();
	[SerializeField, ShowIf("hasChanneledBehaviours")] private Cost channelCost = new Cost();

	private bool UseTriggeredBehaviours {
		get {
			if (!useBehaviourAsLock) {
				return true;
			} else {
				return hasTriggeredBehaviours;
			}
		}
	}

	private float cooldownTimer = 0;
	private bool canContinueChanneling = true;

	protected ToggleState toggleState = ToggleState.Off;
	protected LockState lockState = LockState.None;
	protected Boss boss;

	private void Start() {
		if (useBehaviourAsLock) {
			lockingBehaviour.OnBehaviourEnd += Unlock;
		}
	}

	private void Update() {
		if (lockState.HasFlag(LockState.Active) && lockState.HasFlag(LockState.OnCooldown)) {
			cooldownTimer += Time.deltaTime;
			if (cooldownTimer >= cooldown) {
				lockState ^= LockState.OnCooldown;

				if (!lockState.HasFlag(LockState.Held) && !lockState.HasFlag(LockState.Locked)) {
					Release();
				}
			}
		}
	}

	protected override void TriggerEnter() {
		if (boss == null) {
			boss = triggeringObject.GetComponentInParent<Boss>();
			if (boss == null) {
				return;
			}
		}

		bool canAfford = boss.PayCost(triggerCost);

		if (canAfford && !lockState.HasFlag(LockState.Active)) {
			lockState |= LockState.Active;

			lockState |= LockState.Held;
			StartCoroutine(nameof(ChangeState), true);

			if (useBehaviourAsLock) {
				lockingBehaviour.Trigger();
				lockState |= LockState.Locked;
			}

			if (hasCooldown) {
				cooldownTimer = 0;
				lockState |= LockState.OnCooldown;
			}

			if (UseTriggeredBehaviours) {
				foreach (TriggeredBehaviour behaviour in triggeredBehaviours) {
					if (behaviour != lockingBehaviour) {
						behaviour.Trigger();
					}
				}
			}

			if (hasChanneledBehaviours) {
				canContinueChanneling = true;

				foreach (ContinuousBehaviour behaviour in channeledBehaviours) {
					if (behaviour != lockingBehaviour) {
						behaviour.Trigger();
					}
				}
			}

		}
	}

	protected override void TriggerStay() {
		if (canContinueChanneling) {
			bool canAfford = boss.PayCost(channelCost);

			if (canAfford && lockState.HasFlag(LockState.Active) && hasChanneledBehaviours) {
				foreach (ContinuousBehaviour behaviour in channeledBehaviours) {
					if (behaviour != lockingBehaviour) {
						behaviour.UpdateBehaviour();
					}
				}
			} else if (!canAfford) {
				canContinueChanneling = false;
			}
		}
	}

	protected override void TriggerExit() {
		lockState ^= LockState.Held;

		if (!lockState.HasFlag(LockState.OnCooldown) && !lockState.HasFlag(LockState.Locked)) {
			Release();
		}
	}

	private void Unlock() {
		lockState ^= LockState.Locked;

		if (!lockState.HasFlag(LockState.OnCooldown) && !lockState.HasFlag(LockState.Held)) {
			Release();
		}
	}

	private void Release() {
		if (hasChanneledBehaviours) {
			foreach (ContinuousBehaviour behaviour in channeledBehaviours) {
				if (behaviour != lockingBehaviour) {
					behaviour.EndBehaviour();
				}
			}
		}

		StartCoroutine(nameof(ChangeState), false);
		lockState ^= LockState.Active;
	}

	protected abstract IEnumerator ChangeState(bool toActiveState);

	private void OnDestroy() {
		if (useBehaviourAsLock) {
			lockingBehaviour.OnBehaviourEnd -= Unlock;
		}
	}
}
