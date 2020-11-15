using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButton : TriggerVolume {
	[SerializeField] private float pressSpeed = 2;
	[SerializeField] private Vector3 pressOffset = Vector3.down;
	[SerializeField] private TriggeredBehaviour[] behaviours = null;

	private bool inMotion = false;

	protected override void TriggerEnter() {
		StartCoroutine(nameof(Press));

		foreach (TriggeredBehaviour behaviour in behaviours) {
			behaviour.Trigger();
		}
	}

	protected override void TriggerStay() {

	}

	protected override void TriggerExit() {
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
