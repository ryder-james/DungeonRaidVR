using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousMove : ContinuousBehaviour {
	[SerializeField] private bool resetPosition = true;
	[SerializeField, ShowIf("resetPosition")] private bool lerpToStart = true;
	[SerializeField, ShowIf("lerpToStart")] private float returnSpeed = 10;
	[SerializeField] private Vector3 direction = Vector3.forward;
	[SerializeField] private float speed = 2;

	private bool isMoving = false;
	private Vector3 start;

	protected override void Start() {
		direction.Normalize();
		start = transform.position;
	}

	protected override void Update() {
		if (isMoving) {
			transform.position += direction * speed * Time.deltaTime;
		}
	}

	protected override void Begin() {
		isMoving = true;
	}

	protected override void Channel() {

	}

	protected override void End() {
		isMoving = false;
	}

	protected override void ResetBehaviour() {
		if (resetPosition) {
			if (lerpToStart) {
				StartCoroutine(nameof(LerpTo), start);
			} else {
				transform.position = start;
			}
		}
	}

	private IEnumerator LerpTo(Vector3 position) {
		Vector3 start = transform.position;
		Vector3 end = position;

		for (float t = 0; t < 1; t += Time.deltaTime * returnSpeed) {
			transform.position = Vector3.Lerp(start, end, t);
			yield return new WaitForEndOfFrame();
		}

		transform.position = end;
	}
}
