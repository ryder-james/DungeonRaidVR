using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedBehaviour : ContinuousBehaviour {
	[SerializeField] private float duration = 3;

	private float durationTimer = 0;

	protected override void Update() {
		if (autoUpdating) {
			durationTimer += Time.deltaTime;

			if (durationTimer >= duration) {
				EndBehaviour();
			}
		}
	}

	public override void BeginBehaviour() {
		autoUpdating = true;
		Begin();
	}

	public override void UpdateBehaviour() {
		Channel();
	}

	public override void EndBehaviour() {
		autoUpdating = false;
		End();
	}

	protected override void ResetBehaviour() {
		durationTimer = 0;
	}
}
