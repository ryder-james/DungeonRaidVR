using DungeonRaid.Characters.Bosses.Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerVolume : Interactable {
	[SerializeField] protected string triggerTag = "Boss";

	private const float fixedChannelTimer = 1;

	protected GameObject triggeringObject;

	private float channelTimer = 0;
	private bool held = false;
	private bool interactedThisFrame = false;

	protected abstract void TriggerEnter();
	protected abstract void TriggerStay();
	protected abstract void TriggerExit();

	private void LateUpdate() {
		interactedThisFrame = false;
	}

	protected void ReleaseEarly() {
		held = false;
		TriggerExit();
		triggeringObject = null;
	}

	private void OnTriggerEnter(Collider other) {
		if (!interactedThisFrame && triggeringObject == null && other.CompareTag(triggerTag)) {
			triggeringObject = other.gameObject;
			channelTimer = 0;
			held = true;

			TriggerEnter();

			interactedThisFrame = true;
		}
	}

	private void OnTriggerStay(Collider other) {
		if (!interactedThisFrame && held && other.gameObject == triggeringObject) {
			channelTimer += Time.deltaTime;
			if (channelTimer >= fixedChannelTimer) {
				TriggerStay();
				channelTimer -= fixedChannelTimer;
			}

			interactedThisFrame = true;
		} else if (!interactedThisFrame && !held && other == triggeringObject) {
			OnTriggerExit(other);

			interactedThisFrame = true;
		}
	}

	private void OnTriggerExit(Collider other) {
		if (!interactedThisFrame && other.gameObject == triggeringObject) {
			ReleaseEarly();

			interactedThisFrame = true;
		}
	}
}
