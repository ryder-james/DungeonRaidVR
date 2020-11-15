using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggeredBehaviour : MonoBehaviour {
	public delegate void BehaviourNotification();

	public abstract void Trigger();

	protected virtual void Start() {}

	protected virtual void Update() {}
}
