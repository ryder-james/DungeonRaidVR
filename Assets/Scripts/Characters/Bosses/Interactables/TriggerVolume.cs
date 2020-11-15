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

	protected abstract void TriggerEnter();
	protected abstract void TriggerStay();
	protected abstract void TriggerExit();

	protected void ReleaseEarly() {
		held = false;
		TriggerExit();
		triggeringObject = null;
	}

	private void OnTriggerEnter(Collider other) {
		if (triggeringObject == null && other.CompareTag(triggerTag)) {
			triggeringObject = other.gameObject;
			channelTimer = 0;
			held = true;

			TriggerEnter();
		}
	}

	private void OnTriggerStay(Collider other) {
		if (held && other.gameObject == triggeringObject) {
			channelTimer += Time.deltaTime;
			if (channelTimer >= fixedChannelTimer) {
				TriggerStay();
				channelTimer -= fixedChannelTimer;
			}

		} else if (!held && other == triggeringObject) {
			OnTriggerExit(other);
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject == triggeringObject) {
			ReleaseEarly();
		}
	}
}
