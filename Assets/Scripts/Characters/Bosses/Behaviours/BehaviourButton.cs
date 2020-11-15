using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourButton : TriggerBehaviourToggle {
	[SerializeField] private float pressSpeed = 2;
	[SerializeField] private Vector3 pressOffset = Vector3.down;

	private bool inMotion = false;

	protected override IEnumerator ChangeState(bool releasing) {
		yield return new WaitUntil(() => !inMotion);

		Vector3 start = transform.position;
		Vector3 end = transform.position + (releasing ? -pressOffset : pressOffset);

		inMotion = true;
		for (float t = 0; t < 1; t += Time.deltaTime * pressSpeed) {
			transform.position = Vector3.Lerp(start, end, t);
			yield return new WaitForEndOfFrame();
		}
		inMotion = false;

		transform.position = end;
	}
}
