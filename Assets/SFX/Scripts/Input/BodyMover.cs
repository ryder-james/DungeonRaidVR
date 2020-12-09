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
				Vector3 newVelocity = transform.rotation * (Vector3.ClampMagnitude(direction, 1) * SpeedMultiplier * speed);
				newVelocity.y = body.velocity.y;
				body.velocity = Vector3.ClampMagnitude(newVelocity, SpeedMultiplier * speed);
			} else if (!Manual) {
				body.velocity = new Vector3(0, body.velocity.y, 0);
				body.angularVelocity = Vector3.zero;
			}
		}
}
}
