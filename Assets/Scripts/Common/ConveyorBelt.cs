using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace JCommon.Movement {
	[RequireComponent(typeof(Collider))]
	public class ConveyorBelt : MonoBehaviour {
		[System.Serializable]
		public enum TransformDirection {
			FORWARD,
			BACK,
			LEFT,
			RIGHT,
			UP,
			DOWN
		}

		[SerializeField] private bool useCustomDirection = true;
		[SerializeField, ShowIf("useCustomDirection")] private Vector3 conveyorDirection = Vector3.forward;
		[SerializeField, HideIf("useCustomDirection")] private TransformDirection direction = TransformDirection.FORWARD;
		[SerializeField] private float speed = 3;

		private List<Rigidbody> bodies = new List<Rigidbody>();

		private void Start() {
			conveyorDirection.Normalize();
		}

		private void Update() {
			foreach (Rigidbody body in bodies) {
				body.velocity = GetDirection() * speed;
			}
		}

		private void OnCollisionEnter(Collision collision) {
			Rigidbody body = collision.gameObject.GetComponent<Rigidbody>();
			if (body != null) {
				bodies.Add(body);
			}
		}

		private void OnCollisionExit(Collision collision) {
			Rigidbody body = collision.gameObject.GetComponent<Rigidbody>();
			if (body != null) {
				bodies.Remove(body);
			}
		}

		private void OnDrawGizmosSelected() {
			Gizmos.color = Color.white;
			Vector3 dir = GetDirection();
			Gizmos.DrawLine(transform.position, transform.position + (dir * 10));
		}

		private Vector3 GetDirection() {
			if (useCustomDirection) {
				return conveyorDirection.normalized;
			} else {
				switch (direction) {
				default:
				case TransformDirection.FORWARD:
					return transform.forward;
				case TransformDirection.BACK:
					return -transform.forward;
				case TransformDirection.LEFT:
					return -transform.right;
				case TransformDirection.RIGHT:
					return transform.right;
				case TransformDirection.UP:
					return transform.up;
				case TransformDirection.DOWN:
					return -transform.up;
				}
			}
		}
	}
}

