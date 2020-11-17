using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resettable : MonoBehaviour {
	[SerializeField] private bool resetPosition = true;
	[SerializeField] private bool resetRotation = true;
	[SerializeField] private bool resetScale = true;
	[SerializeField] private bool hasRigidbody = true;
	[SerializeField, Indent, ShowIf("hasRigidbody")] private bool resetVelocity = true;
	[SerializeField, Indent, ShowIf("hasRigidbody")] private bool resetAngularVelocity = true;

	private Dictionary<string, Vector3> resetData;
	private Rigidbody body;

	private void Start() {
		resetData = new Dictionary<string, Vector3>();

		resetData["position"] = transform.localPosition;
		resetData["rotation"] = transform.localRotation.eulerAngles;
		resetData["scale"] = transform.localScale;

		if (hasRigidbody && (resetVelocity || resetAngularVelocity) && TryGetComponent(out body)) {
			resetData["velocity"] = body.velocity;
			resetData["rotVelocity"] = body.angularVelocity;
		} else {
			resetVelocity = false;
			resetAngularVelocity = false;
		}
	}

	public void FullReset() {
		if (resetPosition) {
			transform.localPosition = resetData["position"];
		}

		if (resetRotation) {
			transform.localRotation = Quaternion.Euler(resetData["rotation"]);
		}

		if (resetScale) {
			transform.localScale = resetData["scale"];
		}

		if (hasRigidbody) {
			if (resetVelocity) {
				body.velocity = resetData["velocity"];
			}

			if (resetAngularVelocity) {
				body.angularVelocity = resetData["rotVelocity"];
			}
		}
	}
}
