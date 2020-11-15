using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButton : TriggerVolume {
	[SerializeField] private float pressSpeed = 2;
	[SerializeField] private bool useContinuousBehaviour = false;
	[SerializeField, ShowIf("useContinuousBehaviour"), Indent] private bool waitForBehaviourToEnd = false;
	[SerializeField] private Vector3 pressOffset = Vector3.down;
	[SerializeField, HideIf("useContinuousBehaviour")] private TriggeredBehaviour behaviour = null;
	[SerializeField, ShowIf("useContinuousBehaviour")] private ContinuousBehaviour continuousBehaviour = null;

	private bool inMotion = false;
	private bool isPressed = false;

	private void Start() {
		if (!useContinuousBehaviour) {
			waitForBehaviourToEnd = false;
		}

		if (waitForBehaviourToEnd) {
			continuousBehaviour.OnBehaviourEnd += End;
		}
	}

	protected override void TriggerEnter() {
		if (!isPressed) {
			isPressed = true;
			StartCoroutine(nameof(Press));
			if(useContinuousBehaviour) {
				continuousBehaviour.Trigger();
			} else {
				behaviour.Trigger();
			}
		}
	}

	protected override void TriggerStay() {

	}

	protected override void TriggerExit() {
		if (isPressed && !waitForBehaviourToEnd) {
			End();
		}
	}

	private void End() {
		isPressed = false;
		StartCoroutine(nameof(Release));
	}

	private IEnumerator Press() {
		yield return new WaitUntil(() => !inMotion);

		Vector3 start = transform.position;
		Vector3 end = transform.position + pressOffset;

		inMotion = true;
		for (float t = 0; t < 1; t += Time.deltaTime * pressSpeed) {
			transform.position = Vector3.Lerp(start, end, t);
			yield return new WaitForEndOfFrame();
		}
		inMotion = false;

		transform.position = end;
	}

	private IEnumerator Release() {
		yield return new WaitUntil(() => !inMotion);

		Vector3 start = transform.position;
		Vector3 end = transform.position - pressOffset;

		inMotion = true;
		for (float t = 0; t < 1; t += Time.deltaTime * pressSpeed) {
			transform.position = Vector3.Lerp(start, end, t);
			yield return new WaitForEndOfFrame();
		}
		inMotion = false;

		transform.position = end;
	}
}
