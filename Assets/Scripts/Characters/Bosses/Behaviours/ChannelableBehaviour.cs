using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChannelableBehaviour : MonoBehaviour {
	public delegate void BehaviourNotification();

	public BehaviourNotification OnBehaviourBeginChannel { get; set; }
	public BehaviourNotification OnBehaviourUpdate { get; set; }
	public BehaviourNotification OnBehaviourEndChannel { get; set; }

	public void BeginChannel() {
		OnBehaviourBeginChannel?.Invoke();
		Begin();
	}

	public void ChannelUpdate() {
		OnBehaviourUpdate?.Invoke();
		Channel();
	}

	public void EndChannel() {
		OnBehaviourEndChannel?.Invoke();
		End();
	}

	protected abstract void Begin();
	protected abstract void Channel();
	protected abstract void End();
}
