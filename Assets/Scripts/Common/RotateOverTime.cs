using UnityEngine;

namespace Common.Movement {
	public class RotateOverTime : MonoBehaviour {
		[SerializeField] private Vector3 rotationAxis = Vector3.up;
		[SerializeField] private float rotationSpeed = 2;

		private void Start() {
			rotationAxis.Normalize();
		}

		private void Update() {
			transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
		}
	}
}
