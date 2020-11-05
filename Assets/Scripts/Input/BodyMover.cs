using Sirenix.OdinInspector.Editor.Drawers;
using UnityEngine;

namespace DungeonRaid.Input {
	[RequireComponent(typeof(Rigidbody))]
	public class BodyMover : MonoBehaviour {
		public const float SpeedMultiplier = 5;

		private Rigidbody body;

		public bool Manual { get; set; } = false;
		public float CurrentSpeed => body != null ? body.velocity.magnitude : 0;
		public Vector3 Direction => body != null ? body.velocity.normalized : Vector3.forward;
		public float Angle => Vector3.Angle(transform.forward, Direction);
		public float NormalizedAngle => Angle / 180;

		private void Start() {
			body = GetComponent<Rigidbody>();
		}

		public void MoveToward(Vector3 direction, float speed) {
			if (body == null) {
				return;
			}

			if (direction.magnitude > 0) {
				body.velocity = transform.rotation * (Vector3.ClampMagnitude(direction, 1) * SpeedMultiplier * speed);
				body.velocity = Vector3.ClampMagnitude(body.velocity, SpeedMultiplier * speed);
			} else if (!Manual) {
				body.velocity = Vector3.zero;
				body.angularVelocity = Vector3.zero;
			}
		}
}
}
