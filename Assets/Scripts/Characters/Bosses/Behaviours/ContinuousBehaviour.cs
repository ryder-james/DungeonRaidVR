using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContinuousBehaviour : TriggeredBehaviour {
	public BehaviourNotification OnBehaviourBegin { get; set; }
	public BehaviourNotification OnBehaviourUpdate { get; set; }
	public BehaviourNotification OnBehaviourEnd { get; set; }
	
	protected bool autoUpdating = false;

	private const float fixedUpdateTimer = 1;
	private float updateTimer = 0;

	protected override void Update() {
		if (autoUpdating) {
			updateTimer += Time.deltaTime;

			if (updateTimer >= fixedUpdateTimer) {
				UpdateBehaviour();
				updateTimer -= fixedUpdateTimer;
			}
		}
	}

	public override sealed void Trigger() {
		BeginBehaviour();
	}

	public virtual void BeginBehaviour() {
		ResetBehaviour();
		OnBehaviourBegin?.Invoke();
		Begin();
	}

	public virtual void UpdateBehaviour() {
		OnBehaviourUpdate?.Invoke();
		Channel();
	}

	public virtual void EndBehaviour() {
		OnBehaviourEnd?.Invoke();
		End();
	}

	protected abstract void Begin();
	protected abstract void Channel();
	protected abstract void End();
	protected abstract void ResetBehaviour();
}
