using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTimeLimiter : TimedBehaviour {
	[SerializeField] private ContinuousBehaviour[] behaviours = null;

	protected override void Begin() {
		foreach(ContinuousBehaviour behaviour in behaviours) {
			behaviour.BeginBehaviour();
		}
	}

	protected override void Channel() {
		foreach (ContinuousBehaviour behaviour in behaviours) {
			behaviour.UpdateBehaviour();
		}
	}

	protected override void End() {
		foreach (ContinuousBehaviour behaviour in behaviours) {
			behaviour.EndBehaviour();
		}
	}
}
